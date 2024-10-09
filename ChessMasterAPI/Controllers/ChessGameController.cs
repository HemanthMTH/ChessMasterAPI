using System;
using ChessDotNet;
using ChessMasterAPI.Data;
using ChessMasterAPI.Data.Models;
using ChessGameModel = ChessMasterAPI.Data.Models.ChessGame;
using ChessGame = ChessDotNet.ChessGame;
using ChessMasterAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChessMasterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChessGameController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly StockfishService _stockfishService;
        private readonly PgnToFenService _pgnToFenService;
        private readonly PgnParserService _pgnParserService;

        public ChessGameController(ApplicationDbContext context, StockfishService stockfishService, PgnToFenService pgnToFenService, PgnParserService pgnParserService)
        {
            _context = context;
            _stockfishService = stockfishService;
            _pgnToFenService = pgnToFenService;  
            _pgnParserService = pgnParserService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadGame([FromBody] ChessGameModel model)
        {
            if (ModelState.IsValid)
            {
                string fen;
                try
                {
                    fen = await _pgnToFenService.ConvertPgnToFen(pgnText: model.PGN);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = ex.Message });
                }


                var chessGame = _pgnParserService.ParsePgnMetadata(model.PGN);
                chessGame.FEN = fen;


                _context.ChessGames.Add(model);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Game uploaded successfully!", GameId = model.Id });
            }
            return BadRequest(ModelState);
        }


        [HttpPost("uploadfile")]
        public async Task<IActionResult> UploadGameFile(IFormFile pgnFile)
        {
            if (pgnFile == null || pgnFile.Length == 0)
                return BadRequest(new { Message = "No file uploaded!" });

            string pgnContent;
            string tempPgnFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Tools", "input", "given_pgn.pgn");

            // Ensure input directory exists
            string inputDir = Path.Combine(Directory.GetCurrentDirectory(), "Tools", "input");
            if (!Directory.Exists(inputDir))
            {
                Directory.CreateDirectory(inputDir);
            }

            // Read the PGN content from the uploaded file
            using (var stream = new StreamReader(pgnFile.OpenReadStream()))
            {
                pgnContent = await stream.ReadToEndAsync();
            }

            // Save PGN content to a file for FEN conversion
            await System.IO.File.WriteAllTextAsync(tempPgnFilePath, pgnContent);

            string fen;
            try
            {
                // Convert the PGN file to FEN
                fen = await _pgnToFenService.ConvertPgnToFen(pgnFilePath: tempPgnFilePath);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

            var chessGame = _pgnParserService.ParsePgnMetadata(pgnContent);
            chessGame.FEN = fen;

            _context.ChessGames.Add(chessGame);
            await _context.SaveChangesAsync();

            return Ok(chessGame);
        }

        // GET: api/ChessGame/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGame(Guid id)
        {
            var game = await _context.ChessGames.FindAsync(id);

            if (game == null)
                return NotFound(new { Message = "Game not found!" });

            return Ok(game);
        }

        // GET: api/ChessGame
        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            var games = await _context.ChessGames.ToListAsync();
            return Ok(games);
        }

        // POST: api/ChessGame/Analyze
        [HttpGet("analyze/{id}")]
        public async Task<IActionResult> AnalyzeGame(Guid id)
        {
            var game = await _context.ChessGames.FindAsync(id);

            if (game == null)
                return NotFound(new { Message = "Game not found!" });

            // Get the FEN from the database
            string fen = game.FEN;

            // Use Stockfish to analyze the position
            string analysisResult = await _stockfishService.AnalyzePosition(fen);

            return Ok(new { BestMove = analysisResult });
        }

    }

}
