using System.Diagnostics;

public class PgnToFenService
{
    private readonly string _pgnToFenExePath;

    public PgnToFenService()
    {
        // Set the path to where your PgnToFen.exe is located in your project.
        _pgnToFenExePath = Path.Combine(Directory.GetCurrentDirectory(), "Tools", "PgnToFen.exe");
    }

    // Method to handle both raw PGN text and file paths, and optionally output FEN to a specified file
    public async Task<string> ConvertPgnToFen(string pgnFilePath = null, string pgnText = null)
    {
        string inputDir = Path.Combine(Directory.GetCurrentDirectory(), "Tools", "input");
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Tools", "output");
        string outputFilePath = Path.Combine(outputDir, "fen.txt");

        // Ensure the input and output directories exist
        if (!Directory.Exists(inputDir))
        {
            Directory.CreateDirectory(inputDir);
        }

        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // If PGN text is passed, create a file in the input directory
        if (!string.IsNullOrEmpty(pgnText))
        {
            pgnFilePath = Path.Combine(inputDir, "given_pgn.pgn");
            await File.WriteAllTextAsync(pgnFilePath, pgnText);
        }

        // If no PGN file is provided, raise an error
        if (string.IsNullOrEmpty(pgnFilePath))
        {
            throw new ArgumentNullException(nameof(pgnFilePath), "PGN file path cannot be null or empty.");
        }

        if (!File.Exists(_pgnToFenExePath))
        {
            throw new FileNotFoundException($"The executable {_pgnToFenExePath} could not be found.");
        }

        // Create the arguments
        string arguments = $"--pgnfile \"{pgnFilePath}\" --output \"{outputFilePath}\"";

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = _pgnToFenExePath,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = Path.GetDirectoryName(_pgnToFenExePath)
            }
        };

        try
        {
            process.Start();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to start the process for {_pgnToFenExePath}. Error: {ex.Message}", ex);
        }

        // Capture the output and errors
        string output = await process.StandardOutput.ReadToEndAsync();
        string error = await process.StandardError.ReadToEndAsync();

        // Wait for the process to exit
        await process.WaitForExitAsync(); // Use the async version to avoid blocking

        // Check the exit code and log output/error for debugging
        if (process.ExitCode != 0)
        {
            throw new Exception($"Error during PGN to FEN conversion. Error: {error}. Output: {output}");
        }

        // Read the FEN result from the output file
        string fenResult = await File.ReadAllTextAsync(outputFilePath);

        // Cleanup: Delete the input and output files
        try
        {
            if (File.Exists(pgnFilePath))
            {
                File.Delete(pgnFilePath);
            }

            if (File.Exists(outputFilePath))
            {
                File.Delete(outputFilePath);
            }
        }
        catch (Exception ex)
        {
            // Log or handle any file deletion issues
            Console.WriteLine($"Error during file cleanup: {ex.Message}");
        }

        // Return the FEN result
        return fenResult;
    }
}
