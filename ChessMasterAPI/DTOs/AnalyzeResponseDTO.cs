namespace ChessMasterAPI.DTOs
{
    public class AnalyzeResponseDTO
    {
        public required string BestMove { get; set; }
        public required string CurrentFEN { get; set; }
    }
}
