# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and restore
COPY *.sln ./
COPY src/SensorSimulator/*.csproj ./src/SensorSimulator/
RUN dotnet restore

# Copy everything else and build
COPY . ./
WORKDIR /src/src/SensorSimulator
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/runtime:9.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "SensorSimulator.dll"]
