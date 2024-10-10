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

            try
            {
                stockfishProcess.Start();

                // Send the UCI commands to Stockfish
                using (StreamWriter writer = stockfishProcess.StandardInput)
                {
                    if (writer.BaseStream.CanWrite)
                    {
                        // Send UCI initialization commands
                        await writer.WriteLineAsync("uci");

                        // Send properly formatted position command
                        await writer.WriteLineAsync($"position fen {fen}");

                        // Command to start analysis
                        await writer.WriteLineAsync("go movetime 3000");

                        // Quit after analysis is complete
                        await writer.WriteLineAsync("quit");
                    }
                }

                // Capture the output from Stockfish
                string output = await stockfishProcess.StandardOutput.ReadToEndAsync();
                stockfishProcess.WaitForExit();

                return ParseBestMove(output);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during Stockfish execution: {ex.Message}");
                return "Error occurred";
            }
            finally
            {
                stockfishProcess?.Dispose(); // Ensure process is cleaned up
            }
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
