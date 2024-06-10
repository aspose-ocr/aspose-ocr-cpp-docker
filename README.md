# Aspose.OCR in Docker

This gRPC service provides OCR (Optical Character Recognition) functionality via a wrapper for the Aspose OCR CLI tool. It supports various document area detection modes and can process images provided as local files or URLs.

## Table of Contents
- [Project Overview](#project-overview)
- [Prerequisites](#prerequisites)
- [Building and Running](#building-and-running)
  - [Locally](#locally)
  - [Docker Deployment](#docker-deployment)
- [Usage Instructions](#usage-instructions)
- [API Documentation](#api-documentation)
   - [Process](#process)
   - [HealthCheck](#healthcheck)
   - [Documentation](#documentation)
- [Example Code](#example-code)
   - [grpcurl Examples](Doc/gRPCurl_API.MD)
   - [Python Client Example](Doc/Python_Example.MD)
   - [C# Client Example](Doc/CS_Example.MD)
   - [CLI API](https://releases.aspose.app/ocr/on-premises/)
- [License](#license)

## Project Overview

This project provides a gRPC wrapper for the Aspose OCR CLI tool, exposing a gRPC service with the following endpoints:
- `Process`: Recognizes text from an image using various OCR options.
- `HealthCheck`: Checks the health status of the service.
- `Documentation`: Retrieves the documentation of the service.

## Prerequisites

- .NET 7.0 SDK
- JetBrains Rider IDE
- Docker
- `curl` and `unzip` (for Docker setup)

## Building and Running

### Locally

1. Open the solution in Rider.
2. Build the solution.
3. Run the project.

### Docker Deployment

The service can be easily deployed using Docker. Follow these steps to build and run the Docker container:

### Building the Docker Image

Run the following command to build the Docker image:

```sh
docker build -t ocr_service_image .
```

### Running the Docker Container

You can run the Docker container with the necessary environment variables and volume mounts:

#### With License:

```sh
docker run -d --name ocr_service_container -v /path/to/temp_storage:/temp_storage -e LICENSE_PATH=/temp_storage/Aspose.OCR.lic -e TEMPORARY_STORAGE=/temp_storage ocr_service_image
```

#### Without License:

```sh
docker run -d --name ocr_service_container -v /path/to/temp_storage:/temp_storage -e TEMPORARY_STORAGE=/temp_storage ocr_service_image
```

### Important Notes:

- **Temporary Storage Volume**: The container requires a volume for temporary storage to store intermediate files. Use the `-v /path/to/temp_storage:/temp_storage` option.
- **License File**: If you have an Aspose OCR license, place the license file in your temporary storage defined in previous step. By default, the license file should be named `Aspose.OCR.lic`. Optionally, if you intend to specify strict license file name - set the `LICENSE_PATH` environment variable in Docker container startup configuration accordingly.

## Usage Instructions

Once the Docker container is running, you can interact with the gRPC service using `grpcurl`, client libraries in various languages, or any gRPC client tool.

## API Documentation

### Protobuf

See `protos/ocr_service.proto` for the full Protobuf definition.

### Process

Recognize text from an image using various OCR options.

- **Request**: `OcrRequest`
- **Response**: `OcrResponse`

### HealthCheck

Check the health status of the service.

- **Request**: `EchoRequest`
- **Response**: `OcrResponse`

### Documentation

Retrieve the documentation for the service.

- **Request**: `Empty`
- **Response**: `OcrResponse`

## Example Code:
[grpcurl API](Doc/gRPCurl_API.MD) | [Python Example](Doc/Python_Example.MD) | [C# Example](Doc/CS_Example.MD) | [CLI API](https://releases.aspose.app/ocr/on-premises/)

## License

[MIT](LICENSE)
```
MIT License

Copyright (c) 2024 Aspose Pty Ltd

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```