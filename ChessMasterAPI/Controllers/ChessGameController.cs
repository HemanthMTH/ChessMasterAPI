using ChessMasterAPI.Data;
using ChessMasterAPI.Data.Models;
using ChessMasterAPI.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ChessMasterAPI.Services;

namespace ChessMasterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChessGameController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly StockfishService _stockfishService;
        private readonly PgnParserService _pgnParserService;
        private readonly IMapper _mapper;

        public ChessGameController(ApplicationDbContext context, StockfishService stockfishService, PgnParserService pgnParserService, IMapper mapper)
        {
            _context = context;
            _stockfishService = stockfishService;
            _pgnParserService = pgnParserService;
            _mapper = mapper;
        }

        // Upload a game and link it to the user
        [HttpPost("upload")]
        public async Task<IActionResult> UploadGame([FromBody] ChessGameDTO gameDto)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null) return Unauthorized();

                var chessGame = _mapper.Map<ChessGame>(gameDto);
                chessGame.UserId = userId;

                _context.ChessGames.Add(chessGame);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Game uploaded successfully!", GameId = chessGame.Id });
            }
            return BadRequest(ModelState);
        }

        // Get a specific game by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGame(Guid id)
        {
            var game = await _context.ChessGames
                                     .Include(g => g.User)
                                     .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
                return NotFound(new { Message = "Game not found!" });

            var gameDto = _mapper.Map<ChessGameDTO>(game);
            return Ok(gameDto);
        }

        // Get all games for the logged-in user
        [HttpGet("games")]
        public async Task<IActionResult> GetAllGames()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var games = await _context.ChessGames.Where(g => g.UserId == userId).ToListAsync();

            var gameDtos = _mapper.Map<List<ChessGameDTO>>(games);
            return Ok(gameDtos);
        }

        [HttpPost("uploadfile")]
        public async Task<IActionResult> UploadGameFile(IFormFile pgnFile)
        {
            if (pgnFile == null || pgnFile.Length == 0)
                return BadRequest(new { Message = "No file uploaded!" });

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            string pgnContent;

            using (var stream = new StreamReader(pgnFile.OpenReadStream()))
            {
                pgnContent = await stream.ReadToEndAsync();
            }

            var chessGame = _pgnParserService.ParsePgnMetadata(pgnContent, userId);
            chessGame.UserId = userId;
            _context.ChessGames.Add(chessGame);
            await _context.SaveChangesAsync();

            return Ok(chessGame);
        }

        // Analyze position using Stockfish
        [HttpPost("analyze-position")]
        public async Task<IActionResult> AnalyzePosition([FromBody] AnalyzeRequest analyzeRequest)
        {
            if (string.IsNullOrEmpty(analyzeRequest.FEN))
            {
                return BadRequest(new { Message = "Invalid FEN string!" });
            }

            
            string analysisResult = await _stockfishService.AnalyzePosition(analyzeRequest.FEN);

            AnalyzeResponseDTO responseDTO = new AnalyzeResponseDTO() { BestMove = analysisResult, CurrentFEN = analyzeRequest.FEN };

            return Ok(responseDTO);
        }
    }
}
