# Define the URL of the zip file and the destination directory
$url = "https://github.com/aspose-ocr/Aspose.OCR-for-Cpp-Console/releases/download/23.11.0/Aspose.OCR-for-C++-Console-Windows.23.11.0.zip"
$zipFile = "Aspose.OCR-for-C++-Console-Windows.23.11.0.zip"
$destinationDir = "cli"

# Create the destination directory if it doesn't exist
if (-not (Test-Path -Path $destinationDir)) {
    New-Item -ItemType Directory -Path $destinationDir
}

# Download the zip file
Invoke-WebRequest -Uri $url -OutFile $zipFile

# Unzip the file to the destination directory
Expand-Archive -Path $zipFile -DestinationPath $destinationDir

# Remove the zip file after extraction
Remove-Item -Path $zipFile
