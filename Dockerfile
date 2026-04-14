# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY FixImporter.Core/FixImporter.Core.csproj FixImporter.Core/
COPY FixImporter.Cli/FixImporter.Cli.csproj FixImporter.Cli/
RUN dotnet restore FixImporter.Cli/FixImporter.Cli.csproj

COPY . .
RUN dotnet publish FixImporter.Cli/FixImporter.Cli.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV INPUT_DATA=""
ENTRYPOINT ["dotnet", "FixImporter.Cli.dll"]
