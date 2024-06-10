using System.Diagnostics;
using Grpc.Core;

namespace OcrGrpcWrapper.Services;

public class OcrService: OcrGrpcWrapper.OcrService.OcrServiceBase
{
    private readonly string _temporaryStoragePath;
    private readonly string? _licenseFilePath;
    private readonly string _cliExecutablePath;
    
    public OcrService(string temporaryStoragePath, string? licenseFilePath, string cliExecutablePath)
    {
        _temporaryStoragePath = temporaryStoragePath;
        _licenseFilePath = licenseFilePath;
        _cliExecutablePath = cliExecutablePath;
    }
    
    // Health-Check
    public override Task<OcrResponse> HealthCheck(EchoRequest request, ServerCallContext context)
    {
        return Task.FromResult(new OcrResponse() { Output = request.Message });
    }

    public override Task<OcrResponse> Documentation(Empty request, ServerCallContext context)
    {
        return Task.FromResult(new OcrResponse { Output = OcrGrpcWrapper.Documentation.Md});
    }

    public override async Task<OcrResponse> Process(OcrRequest request, ServerCallContext context)
    { 
        var result = await ExecuteOcrTool(request);
        return new OcrResponse { Output = result };
    }

    private async Task<string> ExecuteOcrTool(OcrRequest request)
    {
        string inputFilePath = Path.Combine(_temporaryStoragePath, Guid.NewGuid() + request.FileType);
        await File.WriteAllBytesAsync(inputFilePath, request.InputFile.ToByteArray());
        
        var args = BuildArguments(inputFilePath, request);
        var startInfo = new ProcessStartInfo
        {
            FileName = _cliExecutablePath,
            Arguments = args,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        string output;
        try
        {
            using var process = System.Diagnostics.Process.Start(startInfo);
            if (process == null)
            {
                throw new Exception("Failed to start OCR process.");
            }
            
            await process.WaitForExitAsync();
            output = await process.StandardOutput.ReadToEndAsync();

            if (process.ExitCode != 0)
            {
                string error = await process.StandardError.ReadToEndAsync();
                throw new Exception($"OCR process failed with exit code {process.ExitCode}: {error}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Critical Error: ", ex);
        }
        finally
        {
            File.Delete(inputFilePath);
        }
        
        return output;
    }

    private string BuildArguments(string inputFilePath, OcrRequest request)
    {
        var arguments = new List<string> { $"--input-file {inputFilePath}" };

        if (!string.IsNullOrWhiteSpace(request.Language))
            arguments.Add($"--language {request.Language}");

        if (!string.IsNullOrWhiteSpace(request.OutputFormat))
            arguments.Add($"--output-format {request.OutputFormat}");

        if (!string.IsNullOrWhiteSpace(request.DetectAreasMode))
            arguments.Add($"--detect-areas-mode {request.DetectAreasMode}");

        if (!string.IsNullOrWhiteSpace(request.AllowedCharacters))
            arguments.Add($"--allowed-characters {request.AllowedCharacters}");

        if (!string.IsNullOrWhiteSpace(request.Alphabet))
            arguments.Add($"--alphabet {request.Alphabet}");

        if (!string.IsNullOrWhiteSpace(request.IgnoredCharacters))
            arguments.Add($"--ignored-characters {request.IgnoredCharacters}");

        if (request.AutoDeskew)
            arguments.Add($"--auto-deskew");

        if (request.Rotate != 0.0f)
            arguments.Add($"--rotate {request.Rotate}");

        if (request.UpscaleSmallFont)
            arguments.Add($"--upscale-small-font");

        if (request.AutoContrast)
            arguments.Add($"--auto-contrast");

        if (request.AutoDenoise)
            arguments.Add($"--auto-denoise");
        
        if (!string.IsNullOrEmpty(_licenseFilePath))  // License were found in temporary_storage
            arguments.Add($" --license-file {_licenseFilePath}");
        

        return string.Join(" ", arguments);
    }
    
}
