FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OcrGrpcWrapper.csproj", "./"]
RUN dotnet restore "OcrGrpcWrapper.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "OcrGrpcWrapper.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Download Aspose OCR CLI tool
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS download
WORKDIR /cli
RUN curl -L -o aspose_ocr.zip https://github.com/aspose-ocr/Aspose.OCR-for-Cpp-Console/releases/download/23.11.0/Aspose.OCR-for-C++-Console-Windows.23.11.0.zip \
    && apt-get update \
    && apt-get install -y unzip \
    && unzip aspose_ocr.zip -d /cli \
    && rm aspose_ocr.zip

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OcrGrpcWrapper.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=download /cli/* /app/

ENV LICENSE_PATH /temp_storage/Aspose.OCR.lic
ENV TEMPORARY_STORAGE /temp_storage
ENV CLI_EXECUTABLE_PATH /app/Aspose.OCR.Cpp.exe

RUN chmod +x CLI_EXECUTABLE_PATH

VOLUME /temp_storage

ENTRYPOINT ["dotnet", "OcrGrpcWrapper.dll"]
