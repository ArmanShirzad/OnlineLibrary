# Use the .NET 8 SDK for the build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy and restore project files
COPY ["OnlineLibrary.API/OnlineLibrary.API.csproj", "OnlineLibrary.API/"]
COPY ["OnlineLibrary.Core/OnlineLibrary.Core.csproj", "OnlineLibrary.Core/"]
COPY ["OnlineLibrary.Application/OnlineLibrary.Application.csproj", "OnlineLibrary.Application/"]
COPY ["OnlineLibrary.Infrastructure/OnlineLibrary.Infrastructure.csproj", "OnlineLibrary.Infrastructure/"]
RUN dotnet restore "OnlineLibrary.API/OnlineLibrary.API.csproj"

# Copy the rest of the project files and build
COPY . .
WORKDIR "/src/OnlineLibrary.API"
RUN dotnet build "OnlineLibrary.API.csproj" -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "OnlineLibrary.API.csproj" -c Release -o /app/publish

# Use the .NET 8 runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineLibrary.API.dll"]
