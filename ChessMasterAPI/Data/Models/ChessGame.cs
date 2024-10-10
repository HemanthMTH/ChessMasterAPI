namespace ChessMasterAPI.Data.Models
{
    public class ChessGame
    {
        public ChessGame(string userId)
        {
            UserId = userId;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }  // Automatically generate a new GUID
        public string PlayerNameWhite { get; set; } = string.Empty; // Ensure default values to avoid null issues
        public string PlayerNameBlack { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public string PGN { get; set; } = string.Empty;
        public string? TimeControl { get; set; }
        public string? WhiteElo { get; set; }
        public string? BlackElo { get; set; }
        public string? Termination { get; set; }
        public string? ECO { get; set; }
        public DateTime? GameDate { get; set; }
        public string? Link { get; set; }
        public string? Site { get; set; }

        public string UserId { get; set; }  
        public User User { get; set; }      // Navigation property
    }
}
