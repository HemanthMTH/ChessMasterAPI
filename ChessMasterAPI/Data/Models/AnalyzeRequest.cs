namespace ChessMasterAPI.Data.Models
{
    public class AnalyzeRequest
    {
        public string GameId { get; set; }
        public string FEN { get; set; }
    }
}
