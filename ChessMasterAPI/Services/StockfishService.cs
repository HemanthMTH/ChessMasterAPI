using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ChessMasterAPI.Services
{
    public class StockfishService
    {
        private readonly string _stockfishPath;

        public StockfishService(string stockfishPath)
        {
            _stockfishPath = stockfishPath; // Path to the Stockfish executable
        }

        // Start the Stockfish process and return the analysis of the given FEN
        public async Task<string> AnalyzePosition(string fen)
        {
            // Initialize Stockfish process
            var stockfishProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = _stockfishPath,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            stockfishProcess.Start();

            // Send the UCI commands to Stockfish
            using (StreamWriter writer = stockfishProcess.StandardInput)
            {
                if (writer.BaseStream.CanWrite)
                {
                    // UCI (Universal Chess Interface) initialization
                    await writer.WriteLineAsync("uci");
                    await writer.WriteLineAsync($"position fen {fen}");
                    await writer.WriteLineAsync("go depth 20"); // Analyze up to depth 20
                    await writer.WriteLineAsync("quit");
                }
            }

            // Capture the output from Stockfish
            string output = await stockfishProcess.StandardOutput.ReadToEndAsync();
            stockfishProcess.WaitForExit();

            return ParseBestMove(output);
        }
        
        // Extract the best move from the Stockfish output
        private string ParseBestMove(string stockfishOutput)
        {
            var lines = stockfishOutput.Split('\n');
            foreach (var line in lines)
            {
                if (line.StartsWith("bestmove"))
                {
                    return line.Split(' ')[1]; // Extract the move
                }
            }

            return "No move found";
        }
    }
}
