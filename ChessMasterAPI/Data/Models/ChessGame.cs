namespace ChessMasterAPI.Data.Models
{
    public class ChessGame
    {
        public Guid Id { get; set; } = Guid.NewGuid();  // Automatically generate a new GUID
        public string PlayerNameWhite { get; set; }     // White player name
        public string PlayerNameBlack { get; set; }     // Black player name
        public string? Result { get; set; }              // Game result
        public string? TimeControl { get; set; }         // Time control
        public string? WhiteElo { get; set; }            // White player's Elo rating
        public string? BlackElo { get; set; }            // Black player's Elo rating
        public string? Termination { get; set; }         // Game termination reason
        public string? ECO { get; set; }                 // Opening code (ECO)
        public DateTime? GameDate { get; set; }          // Game date
        public string? Link { get; set; }                // Game link

        public string? Site { get; set; }                // Site where the game was played

        public string PGN { get; set; }                 // PGN content
        public string FEN { get; set; }                 // FEN (after conversion)
    }

}
