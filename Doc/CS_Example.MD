# gRPC OCR Client Example

This is a simple C# client for interacting with a gRPC OCR service. The client demonstrates how to call the OCR, health check, and documentation endpoints provided by the service.

## Prerequisites

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0) or newer.
- A running instance of the gRPC OCR service / Docker (adjust `serverAddress` if necessary)

## Setup

1. **Create a New Console Application**

    ```sh
    dotnet new console -o OcrClientApp
    cd OcrClientApp
    ```

2. **Add gRPC and Protobuf NuGet Packages**

    ```sh
    dotnet add package Grpc.Net.Client
    dotnet add package Google.Protobuf
    dotnet add package Grpc.Tools
    ```

3. **Add the `OcrClient.cs` File**

   Create a new file called `OcrClient.cs` and add the following content:

    ```csharp
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Google.Protobuf;
    using Grpc.Net.Client;
    using OcrGrpcWrapper;

    class Program
    {
        static async Task Main(string[] args)
        {
            // The address of the gRPC server
            string serverAddress = "http://localhost:5000";

            // Path of the input image file
            string imagePath = "example.png";

            // Read image file as byte array
            byte[] imageBytes = await File.ReadAllBytesAsync(imagePath);

            // Create a channel and a client
            using var channel = GrpcChannel.ForAddress(serverAddress);
            var client = new OcrService.OcrServiceClient(channel);

            // Create an OcrRequest message
            var request = new OcrRequest
            {
                InputFile = ByteString.CopyFrom(imageBytes),
                Language = "eng",
                OutputFormat = "json",
                DetectAreasMode = "document",
                AutoDeskew = true,
                Rotate = 0,
                UpscaleSmallFont = true,
                AutoContrast = true,
                AutoDenoise = true
            };

            // Call the Process method
            OcrResponse response = await client.ProcessAsync(request);

            // Display the result
            Console.WriteLine("OCR Result:");
            Console.WriteLine(response.Output);

            // Call the HealthCheck method
            var healthCheckRequest = new EchoRequest { Message = "Are you there?" };
            OcrResponse healthCheckResponse = await client.HealthCheckAsync(healthCheckRequest);

            // Display the health check response
            Console.WriteLine("Health Check Response:");
            Console.WriteLine(healthCheckResponse.Output);

            // Call the Documentation method
            OcrResponse documentationResponse = await client.DocumentationAsync(new Empty());

            // Display the documentation response
            Console.WriteLine("Documentation Response:");
            Console.WriteLine(documentationResponse.Output);
        }
    }
    ```

4. **Add the Proto File**

   Create a new directory called `Protos` and add your `ocr_service.proto` file in it. Ensure it is included in your project file as follows:

    ```xml
    <ItemGroup>
      <Protobuf Include="Protos\ocr_service.proto" GrpcServices="Client" />
    </ItemGroup>
    ```

5. **Run the Application**

   Ensure that you have a valid image file named `example.png` in the project directory.

    ```sh
    dotnet run
    ```

## Example Output

Your console application should print the results of the OCR processing along with the health check and documentation responses.

## Notes

- Adjust the `serverAddress` in the code if your gRPC server is running on a different host or port.
- The `input_file` must be a valid image file supported by the OCR service (PNG, JPEG, BMP, or TIFF).