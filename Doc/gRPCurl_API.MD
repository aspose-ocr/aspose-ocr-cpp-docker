## gRPC Service Documentation

### Overview

This gRPC service provides OCR (Optical Character Recognition) functionality via a wrapper for the Aspose OCR CLI tool. It also includes health check and documentation endpoints. The service is defined as follows:

### Proto File

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
  string language = 2;
  string output_format = 3;
  string detect_areas_mode = 4;
  string allowed_characters = 5;
  string alphabet = 6;
  string ignored_characters = 7;
  bool auto_deskew = 8;
  double rotate = 9;
  bool upscale_small_font = 10;
  bool auto_contrast = 11;
  bool auto_denoise = 12;
}

message OcrResponse {
  string output = 1;
}

message EchoRequest {
  string message = 1;
}

message Empty {}
```

### RPC Methods

#### Process

Recognize text from an image using various OCR options.

- **Request**: `OcrRequest`
    - `bytes input_file`: The image file (PNG, JPEG, BMP, or TIFF) to process.
    - `string file_type`:  The type of the input file. Supported file types are: ".png .jpg .jpeg .bmp .ico .zip .tif .tiff .pdf .djvu"
    - `string language`: The language of the text in the source image.
    - `string output_format`: The format of the output (text, json, xml).
    - `string detect_areas_mode`: The algorithm for detecting, ordering, and classifying content blocks.
    - `string allowed_characters`: Predefined whitelist of characters the recognition engine will look for.
    - `string alphabet`: A case-sensitive list of characters to be recognized.
    - `string ignored_characters`: A blacklist of characters that are ignored during recognition.
    - `bool auto_deskew`: Automatically correct image tilt.
    - `double rotate`: Image rotation angle in degrees.
    - `bool upscale_small_font`: Improve small font recognition.
    - `bool auto_contrast`: Automatically improve the contrast.
    - `bool auto_denoise`: Automatically reduce noise.
- **Response**: `OcrResponse`
    - `string output`: The recognized text or data in the specified format.

For **`string file_type`** : Supported file types are:
".png" (SingleImage)
".jpg" (SingleImage)
".jpeg" (SingleImage)
".bmp" (SingleImage)
".ico" (SingleImage)
".zip" (Zip)
".tif" (TIFF)
".tiff" (TIFF)
".pdf" (PDF)
".djvu" (DJVU)

For **`string language`** : Supported file types are:
[See Aspose.OCR C++ Documentation](https://docs.aspose.com/ocr/cpp/languages/)

#### Notes

- input_file: Ensure the file is properly encoded in base64 format.
- file_type: Make sure to use the correct file extension from the supported types.
- All other fields are optional and can be adjusted based on the specific requirements of the OCR process.

#### HealthCheck

Check the health status of the service.

- **Request**: `EchoRequest`
    - `string message`: A message to be echoed back.
- **Response**: `OcrResponse`
    - `string output`: Echos back the input message.

#### Documentation

Retrieve the documentation for the service.

- **Request**: `Empty`
- **Response**: `OcrResponse`
    - `string output`: The documentation of the service.

### Examples Using `grpcurl`

#### Example: Process an OCR Request

Assuming you have an image file named `example.png` in the current directory.

```sh
# Encode the image file to base64 and pass to grpcurl
base64_example=$(base64 -w 0 example.png)
grpcurl -plaintext -d '{
  "input_file": "'"$base64_example"'",
  "file_type": ".png",
  "language": "eng",
  "output_format": "json",
  "detect_areas_mode": "document",
  "auto_deskew": true,
  "rotate": 0,
  "upscale_small_font": true,
  "auto_contrast": true,
  "auto_denoise": true
}' localhost:5000 ocr.OcrService/Process
```

#### Example: Health Check

```sh
grpcurl -plaintext -d '{"message": "Are you there?"}' localhost:5000 ocr.OcrService/HealthCheck
```

Expected response:

```json
{
  "output": "Are you there?"
}
```

#### Example: Retrieve Documentation

```sh
grpcurl -plaintext -d '{}' localhost:5000 ocr.OcrService/Documentation
```

Expected response:

```json
{
  "output": "Detailed documentation of the gRPC OCR service."
}
```

### Common Scenarios

#### Detect Text from an Image (English)

```sh
base64_example=$(base64 -w 0 example.png)
grpcurl -plaintext -d '{
  "input_file": "'"$base64_example"'",
  "file_type": ".png",
  "language": "eng"
}' localhost:5000 ocr.OcrService/Process
```

#### Detect Text with Enhanced Contrast and Noise Reduction

```sh
base64_example=$(base64 -w 0 example.png)
grpcurl -plaintext -d '{
  "input_file": "'"$base64_example"'",
  "file_type": ".png",
  "auto_contrast": true,
  "auto_denoise": true
}' localhost:5000 ocr.OcrService/Process
```

### Notes:

1. Make sure you have `grpcurl` installed on your machine.
2. The default gRPC server is running on `localhost:5000`. Adjust the host and port accordingly if your server configuration is different.
3. The `input_file` must be provided in base64-encoded format. The bash command when using `grpcurl` works to read, encode, and include the file in the request.


## Variuos grpcurl Examples

### Example 1: Recognize Text from a French Image and Return XML Output

Assuming you have an image file named `french_example.png` in the current directory.

```sh
# Encode the image file to base64 and pass to grpcurl
base64_example=$(base64 -w 0 french_example.png)
grpcurl -plaintext -d '{
  "input_file": "'"$base64_example"'",
  "file_type": ".png",
  "language": "fra",
  "output_format": "xml",
  "detect_areas_mode": "document",
  "auto_deskew": false,
  "rotate": 0,
  "upscale_small_font": false,
  "auto_contrast": false,
  "auto_denoise": false
}' localhost:5000 ocr.OcrService/Process
```

### Example 2: Recognize Text from an Image and Use Curved Text Mode

```sh
# Encode the image file to base64 and pass to grpcurl
base64_example=$(base64 -w 0 example.png)
grpcurl -plaintext -d '{
  "input_file": "'"$base64_example"'",
  "file_type": ".png",
  "language": "eng",
  "output_format": "json",
  "detect_areas_mode": "curved_text",
  "auto_deskew": true,
  "rotate": 0,
  "upscale_small_font": true,
  "auto_contrast": true,
  "auto_denoise": true
}' localhost:5000 ocr.OcrService/Process
```

### Example 3: Recognize Text from a URL Image and Return Plain Text

Assuming the image is accessible at a public URL.

```sh
grpcurl -plaintext -d '{
  "input_file": "'"https://example.com/image.png"'",
  "file_type": ".png",
  "language": "eng",
  "output_format": "text",
  "detect_areas_mode": "document",
  "auto_deskew": false,
  "rotate": 0,
  "upscale_small_font": false,
  "auto_contrast": false,
  "auto_denoise": false
}' localhost:5000 ocr.OcrService/Process
```

### Example 4: Recognize Text from an Image with a Custom Alphabet

```sh
# Encode the image file to base64 and pass to grpcurl
base64_example=$(base64 -w 0 example.png)
grpcurl -plaintext -d '{
  "input_file": "'"$base64_example"'",
  "file_type": ".png",
  "language": "eng",
  "output_format": "json",
  "alphabet": "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789",
  "auto_deskew": true,
  "rotate": 0,
  "upscale_small_font": true,
  "auto_contrast": false,
  "auto_denoise": false
}' localhost:5000 ocr.OcrService/Process
```

### Example 5: Recognize Text from an Image with Specific Allowed Characters

```sh
# Encode the image file to base64 and pass to grpcurl
base64_example=$(base64 -w 0 example.png)
grpcurl -plaintext -d '{
  "input_file": "'"$base64_example"'",
  "file_type": ".png",
  "language": "eng",
  "output_format": "json",
  "allowed_characters": "digits",
  "auto_deskew": true,
  "rotate": 0,
  "upscale_small_font": true,
  "auto_contrast": true,
  "auto_denoise": true
}' localhost:5000 ocr.OcrService/Process
```

## Additional grpcurl Examples with `detect-areas-mode`

### Overview of `detect-areas-mode`

- **document (default)**: Best for large, multi-column documents, books, articles, and contracts.
- **photo**: Ideal for photographs or screenshots, where text may be scattered in small lines or blocks.
- **combine**: Combines the document and photo modes to handle complex images with isolated text blocks.
- **table**: Suited for scanned spreadsheets, reports, or any other structured tabular content.
- **curved_text**: Perfect for images with curved text lines, such as those from photographs of books or documents with page distortions.

### Example 1: Detecting Text in a Document (default mode)

This is the default mode and is best for large multi-column documents, books, articles, and contracts.

```sh
# Encode the image file to base64 and pass to grpcurl
base64_example=$(base64 -w 0 document_example.png)
grpcurl -plaintext -d '{
  "input_file": "'"$base64_example"'",
  "file_type": ".png",
  "language": "eng",
  "output_format": "json",
  "detect_areas_mode": "document"
}' localhost:5000 ocr.OcrService/Process
```

### Example 2: Detecting Text in a Photo

This mode works well with lines and individual words inside photos or screenshots.

```sh
# Encode the image file to base64 and pass to grpcurl
base64_example=$(base64 -w 0 photo_example.png)
grpcurl -plaintext -d '{
  "input_file": "'"$base64_example"'",
  "file_type": ".png",
  "language": "eng",
  "output_format": "json",
  "detect_areas_mode": "photo"
}' localhost:5000 ocr.OcrService/Process
```

### Example 3: Detecting Combined Text in Complex Images

This mode combines document and photo detection capabilities to handle complex images with isolated text blocks.

```sh
# Encode the image file to base64 and pass to grpcurl
base64_example=$(base64 -w 0 complex_example.png)
grpcurl -plaintext -d '{
  "input_file": "'"$base64_example"'",
  "file_type": ".png",
  "language": "eng",
  "output_format": "json",
  "detect_areas_mode": "combine"
}' localhost:5000 ocr.OcrService/Process
```

### Example 4: Detecting Text in Tabular Structures

This mode is perfect for detecting cells in tabular structures like tables and invoices.

```sh
# Encode the image file to base64 and pass to grpcurl
base64_example=$(base64 -w 0 table_example.png)
grpcurl -plaintext -d '{
  "input_file": "'"$base64_example"'",
  "file_type": ".png",
  "language": "eng",
  "output_format": "json",
  "detect_areas_mode": "table"
}' localhost:5000 ocr.OcrService/Process
```

### Example 5: Detecting Text in Curved Text Lines

This mode auto-straightens and finds text blocks in images with curved lines, such as those in photographs of books.

```sh
# Encode the image file to base64 and pass to grpcurl
base64_example=$(base64 -w 0 curved_text_example.png)
grpcurl -plaintext -d '{
  "input_file": "'"$base64_example"'",
  "file_type": ".png",
  "language": "eng",
  "output_format": "json",
  "detect_areas_mode": "curved_text"
}' localhost:5000 ocr.OcrService/Process
```

### Recap of Basic grpcurl Examples for Convenience

#### Recognize a Simple Image (English only)

```sh
# Encode the image file to base64 and pass to grpcurl
base64_example=$(base64 -w 0 example.png)
grpcurl -plaintext -d '{
  "input_file": "'"$base64_example"'",
  "file_type": ".png",
  "language": "eng"
}' localhost:5000 ocr.OcrService/Process
```


```