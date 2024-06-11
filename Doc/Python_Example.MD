# gRPC OCR Client Example (Python)

This is a simple Python client for interacting with a gRPC OCR service. The client demonstrates how to call the OCR, health check, and documentation endpoints provided by the service.

## Prerequisites

- [Python 3.6+](https://www.python.org/downloads/)
- `grpcio` and `grpcio-tools` packages
- A running instance of the gRPC OCR service (adjust `server_address` if necessary)

## Setup

1. **Install gRPC tools**

    ```sh
    pip install grpcio grpcio-tools googleapis-common-protos
    ```

2. **Create the Proto File**

    Create a new directory called `protos` and add your `ocr_service.proto` file inside it.

    ```proto
    syntax = "proto3";
    option csharp_namespace = "OcrGrpcWrapper";
    package ocr;

    service OcrService {
      rpc Process (OcrRequest) returns (OcrResponse);
      rpc HealthCheck (EchoRequest) returns (OcrResponse);
      rpc Documentation (Empty) returns (OcrResponse);
    }

message OcrRequest {
  bytes input_file = 1;
  string file_type = 2;
  string language = 3;
  string output_format = 4;
  string detect_areas_mode = 5;
  string allowed_characters = 6;
  string alphabet = 7;
  string ignored_characters = 8;
  bool auto_deskew = 9;
  double rotate = 10;
  bool upscale_small_font = 11;
  bool auto_contrast = 12;
  bool auto_denoise = 13;
}

    message OcrResponse {
      string output = 1;
    }

    message EchoRequest {
      string message = 1;
    }

    message Empty {}
    ```

3. **Generate gRPC Code from Proto File**

    From the directory containing the `protos` directory, run:

    ```sh
    python -m grpc_tools.protoc -I=protos --python_out=. --grpc_python_out=. protos/ocr_service.proto
    ```

    This will generate `ocr_service_pb2.py` and `ocr_service_pb2_grpc.py` files in your current directory.

4. **Create the Python Client Code**

    Create a new file called `ocr_client.py` and add the following content:

    ```python
    import grpc
    import base64
    from google.protobuf.empty_pb2 import Empty
    from ocr_service_pb2 import OcrRequest, EchoRequest
    from ocr_service_pb2_grpc import OcrServiceStub

    def run():
        # The address of the gRPC server
        server_address = 'localhost:5000'

        # Path of the input image file
        image_path = 'example.png'

        # Read the image file as bytes
        with open(image_path, 'rb') as image_file:
            image_bytes = image_file.read()

        # Create a gRPC channel and a client
        channel = grpc.insecure_channel(server_address)
        client = OcrServiceStub(channel)

        # Create an OcrRequest message
        ocr_request = OcrRequest(
            input_file=image_bytes,
            file_type='png',
            language='eng',
            output_format='json',
            detect_areas_mode='document',
            auto_deskew=True,
            rotate=0,
            upscale_small_font=True,
            auto_contrast=True,
            auto_denoise=True,
        )

        # Call the Process method
        ocr_response = client.Process(ocr_request)
        print('OCR Result:')
        print(ocr_response.output)

        # Call the HealthCheck method
        echo_request = EchoRequest(message='Are you there?')
        health_check_response = client.HealthCheck(echo_request)
        print('Health Check Response:')
        print(health_check_response.output)

        # Call the Documentation method
        documentation_response = client.Documentation(Empty())
        print('Documentation Response:')
        print(documentation_response.output)

    if __name__ == '__main__':
        run()
    ```

5. **Run the Application**

    Ensure you have a valid image file named `example.png` in the current directory and then run:

    ```sh
    python ocr_client.py
    ```

## Example Output

Your console should display the results of the OCR processing, along with the health check and documentation responses.

## Notes

- Adjust the `server_address` in the code if your gRPC server is running on a different host or port.
- The `input_file` must be a valid image file supported by the OCR service (PNG, JPEG, BMP, or TIFF).
