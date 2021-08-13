# Builder
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS builder 
WORKDIR /source

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish --output package --configuration release

# Production
# FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /app
COPY --from=builder /source/package .
ENTRYPOINT ["dotnet", "DemoDataGenerator.dll"]