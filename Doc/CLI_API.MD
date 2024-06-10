# Refer to the latest version od this doc on [Aspose Micro Apps](https://releases.aspose.app/ocr/on-premises/)

# Aspose.OCR On-premises Micro App

A fast and easy-to-use utility for optical character recognition from the Windows terminal, Linux console, batch files, PowerShell, or Bash scripts. Convert images to text without writing a single line of code and easily extend your automated workflows with OCR functionality.

[Aspose.OCR technology](https://products.aspose.com/ocr/cpp/) | [Blog](https://blog.aspose.com/category/ocr/) | [Free Support](https://forum.aspose.com/c/ocr/16) | [Licensing](https://purchase.aspose.com/temporary-license/) | [Documentation](https://docs.aspose.com/ocr/cpp/)

Aspose.OCR is a software development kit for optical character recognition. It allows you to add OCR functionality to your applications written in .NET, Java, C++, Python and JavaScript.

This command-line utility powered by Aspose.OCR for C++ provides a quick and easy way to test our OCR engine without writing a single line of code. Simply run it from the Windows terminal or PowerShell console or include it to the batch file.

Download the source code at https://github.com/aspose-ocr/Aspose.OCR-for-Cpp-Console/releases to familiarize yourself with the API and write your own applications quickly and easily.

## System requirements

The utility was tested under the following operating systems:

- Microsoft Windows 10 or above;
- Microsoft Windows Server 2016 or above;
- Ubuntu 20.04.

The product package already includes all the necessary components and libraries. Do not move, rename or delete any files in it.

## Installation

1. Download the latest version of the executable package for your operating system.
2. Extract the archive to some folder on your machine.
3. Run the utility from the command line or batch file.

## Licensing

You can start using Aspose.OCR On-premises Micro App right after the installation. While the utility itself is distributed as an open-source under the [MIT license](https://opensource.org/license/mit/), the Aspose.OCR recognition engine has some restrictions:

- If the number of characters in a recognized image exceeds 300, only the first 300 characters are recognized.
- If the number of characters in a recognized image is less than 300, the first 60% of characters are recognized.

Apply a [temporary license](https://purchase.aspose.com/temporary-license) or [purchase](https://purchase.aspose.com/pricing/ocr/cpp) a full license to remove the restrictions.

## Usage

For the Aspose.OCR On-premises Micro App to work, you must provide an absolute or relative path to the source content that will be sent to OCR:

`--input-file <path>`

The following content types are supported and automatically detected:

- _Image file_  
  One of the following image files: PNG, JPEG, BMP, or TIFF.
- _Public URL_  
  Direct web link to PNG, JPEG, BMP, or TIFF image. The utility does not support authentication and can only work with public URLs.
- _Archive_  
  ZIP archive with any number of PNG, JPEG, BMP, or TIFF files. Archived folders and nested archives are not processed.
- _Folder_  
  Folder with any number of  PNG, JPEG, BMP, TIFF images, or ZIP archives. Subfolders are not processed.

### Optional parameters

All below-mentioned parameters are optional. If omitted, the default value is applied. Textual values can be specified in either lower or upper case.

#### `--license-file <path>`

Absolute or relative path to the Aspose.OCR for C++ license. See **Licensing** chapter for more information.

#### `--language <code>`

The language of the text in the source image. Make sure you specify the correct language, otherwise only characters that look the same as those in the selected language will be recognized correctly.

This parameter can be shortened to `-l <code>`.

The following languages are supported:

- **none** (default) - Extended Latin characters, including diacritics
- **bel** - Belorussian
- **bul** - Bulgarian
- **chi** - Chinese
- **cze** - Czech
- **dan** - Danish
- **deu** - German
- **dum** - Dutch
- **eng** - English
- **est** - Estonian
- **fin** - Finnish
- **fra** - French
- **hin** - Hindi
- **ita** - Italian
- **kaz** - Kazakh
- **lav** - Latvian
- **lit** - Lithuanian
- **nor** - Norwegian
- **pol** - Polish
- **por** - Portuguese
- **rum** - Romanian
- **rus** - Russian
- **slk** - Slovak
- **slv** - Slovene
- **spa** - Spanish
- **srp** - Serbian
- **srp_hrv** - Serbo-Croatian
- **swe** - Swedish
- **ukr** - Ukrainian

#### `--output-format <format>`

Output data format. This parameter can be shortened to `-o <format>`.

The following formats are supported:

- **text** (default) - plain text;
- **json** - JSON;
- **xml** - XML.

#### `--detect-areas-mode <OCR mode>`

An algorithm for detecting, ordering, and classifying content blocks on a page. Choose the one that works best for your specific content. Read https://docs.aspose.com/ocr/cpp/areas-detection/ for details.

This parameter can be shortened to `-m <OCR mode>`.

The following values are supported:

- **document** (default) - large multi-column documents, books, articles, contracts.
- **photo** - lines and individual words inside photos or screenshots.
- **combine** - isolated blocks of structured text in complex images. Extracts as much text from an image as possible.
- **table** - scanned spreadsheets, reports, and other tabular structures.
- **curved_text** - photos of curved pages or ultra-wide lens distortions.

#### `--allowed-characters <preset>`

Predefined whitelist of characters the recognition engine will look for.

This parameter can be shortened to `-b <preset>`.

The following presets are supported:

- **all** (default) - all characters.
- **latin_alphabet** - case-insensitive Latin/English text (A to Z and a to z), without accented characters.
- **digits** - decimal, binary, octal, or hexadecimal numbers (0-9 and A to F).

#### `--alphabet <characters>`

A case-sensitive list of characters to be recognized, provided as a string. Characters that do not match the provided list are ignored.

This parameter can be shortened to `-a <characters>`.

#### `--ignored-characters <characters>`

A blacklist of characters that are ignored during recognition. By default, all characters are recognized.

This parameter can be shortened to `-d <characters>`.

#### `--auto-deskew`

Set this flag to automatically correct image tilt (deskew) before proceeding to recognition.

You can use `-s` shorthand.

Disabled by default.

#### `--rotate <angle>`

Set image rotation angle in degrees (decimal number with dot separator). Applied before the image is recognized to improve the accuracy.

This parameter can be shortened to `-r <angle>`.

By default, the image is not rotated.

#### `--upscale-small-font`

Improve small font recognition and detection of dense lines.

You can use `-u` shorthand.

Disabled by default.

#### `--auto-contrast`

Automatically improve the contrast before proceeding to recognition.

You can use `-c` shorthand.

Disabled by default.

#### `--auto-denoise`

Automatically improve the contrast before proceeding to recognition.

You can use `-n` shorthand.

Disabled by default.

## Examples

1. Quickly recognize a simple image (English-only):  
   `Aspose.OCR.Cpp.exe --input-file source.png`

2. Apply a trial license:  
   `Aspose.OCR.Cpp.exe --license-file Aspose.OCR.Trial.lic --input-file source.png`

3. Extract content from French image in JSON format:  
   `Aspose.OCR.Cpp.exe --input-file source.png --language fra --output-format json`

4. Enhance a photo and extract text from it:  
   `Aspose.OCR.Cpp.exe --input-file photo.png --detect-areas-mode photo --auto-deskew --auto-denoise`

5. Extract text from all scanned pages in a folder:  
   `Aspose.OCR.Cpp.exe --input-file C:\images --detect-areas-mode document --language ukr`

6. Extract text from a web link without downloading a file to your computer:  
   `Aspose.OCR.Cpp.exe --input-file https://docs.aspose.com/ocr/cpp/hello-world/source.png`