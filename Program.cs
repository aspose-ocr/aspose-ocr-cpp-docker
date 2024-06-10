using OcrGrpcWrapper.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Retrieve necessary environment variables
string temporaryStoragePath = Environment.GetEnvironmentVariable("TEMPORARY_STORAGE") 
                              ?? throw new InvalidOperationException("TEMPORARY_STORAGE environment variable is not set.");
string cliExecutablePath = Environment.GetEnvironmentVariable("CLI_EXECUTABLE_PATH")
                           ?? throw new InvalidOperationException("CLI_EXECUTABLE_PATH environment variable is not set.");
string? licenseFilePath = Environment.GetEnvironmentVariable("LICENSE_PATH");

if (!Directory.Exists(temporaryStoragePath))
{
    Directory.CreateDirectory(temporaryStoragePath);
}

// gRPC server MUST have server reflection enabled.
// Server reflection is necessary for grpcurl to discover the services and methods available on the server.
builder.Services.AddSingleton(new OcrService(temporaryStoragePath, licenseFilePath, cliExecutablePath));
// Add gRPC services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

var app = builder.Build();

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.MapGrpcService<OcrService>();

// Enable reflection
app.MapGrpcReflectionService();

app.Run();