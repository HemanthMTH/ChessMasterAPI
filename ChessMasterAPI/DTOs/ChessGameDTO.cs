// Ignore Spelling: Elo

namespace ChessMasterAPI.DTOs
{
    public class ChessGameDTO
    {
        public Guid Id { get; set; }
        public required string UserId { get; set; }
        public required string PlayerNameWhite { get; set; }
        public required string PlayerNameBlack { get; set; }
        public required string Result { get; set; }
        public string? TimeControl { get; set; }
        public string? WhiteElo { get; set; }
        public string? BlackElo { get; set; }
        public string? Termination { get; set; }
        public string? ECO { get; set; }
        public DateTime? GameDate { get; set; }
        public string? Link { get; set; }
        public string? Site { get; set; }
        public string PGN { get; set; }
    }
}
