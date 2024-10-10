// Ignore Spelling: Metadata

using ChessMasterAPI.Data.Models;

namespace ChessMasterAPI.Services
{
    public class PgnParserService
    {
        // Method to parse PGN metadata
        public ChessGame ParsePgnMetadata(string pgnContent, string userId)
        {
            var chessGame = new ChessGame(userId);

            // Extract metadata from the PGN header
            var lines = pgnContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                if (line.StartsWith("[Event ")) continue; 
                else if (line.StartsWith("[Site "))
                {
                    chessGame.Site = line.Split('"')[1];
                }
                else if (line.StartsWith("[Date "))
                {
                    string dateStr = line.Split('"')[1];
                    if (DateTime.TryParse(dateStr, out DateTime gameDate))
                    {
                        chessGame.GameDate = gameDate;
                    }
                }
                else if (line.StartsWith("[White "))
                {
                    chessGame.PlayerNameWhite = line.Split('"')[1];
                }
                else if (line.StartsWith("[Black "))
                {
                    chessGame.PlayerNameBlack = line.Split('"')[1];
                }
                else if (line.StartsWith("[Result "))
                {
                    chessGame.Result = line.Split('"')[1];
                }
                else if (line.StartsWith("[TimeControl "))
                {
                    chessGame.TimeControl = line.Split('"')[1];
                }
                else if (line.StartsWith("[WhiteElo "))
                {
                    chessGame.WhiteElo = line.Split('"')[1];
                }
                else if (line.StartsWith("[BlackElo "))
                {
                    chessGame.BlackElo = line.Split('"')[1];
                }
                else if (line.StartsWith("[Termination "))
                {
                    chessGame.Termination = line.Split('"')[1];
                }
                else if (line.StartsWith("[ECO "))
                {
                    chessGame.ECO = line.Split('"')[1];
                }
                else if (line.StartsWith("[Link "))
                {
                    chessGame.Link = line.Split('"')[1];
                }
            }

            chessGame.PGN = pgnContent;

            return chessGame;
        }
    }
}
