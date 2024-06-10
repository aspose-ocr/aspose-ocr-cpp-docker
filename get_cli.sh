#!/bin/bash

# Define variables
URL="https://github.com/aspose-ocr/Aspose.OCR-for-Cpp-Console/releases/download/23.11.0/Aspose.OCR-for-C++-Console-Windows.23.11.0.zip"
ZIP_FILE="Aspose.OCR-for-C++-Console-Windows.23.11.0.zip"
DEST_DIR="cli"

# Create destination directory if it doesn't exist
mkdir -p "$DEST_DIR"

# Download the file
curl -L -o "$ZIP_FILE" "$URL"

# Unzip the file into the destination directory
unzip "$ZIP_FILE" -d "$DEST_DIR"

# Clean up by removing the downloaded zip file
rm "$ZIP_FILE"

echo "Download and extraction completed successfully."
