# --- Build Stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src
# Copy csproj and restore dependencies
COPY ["BugTracker.API/BugTracker.API.csproj", "BugTracker.API/"]
RUN dotnet restore "BugTracker.API/BugTracker.API.csproj"

# Copy everything else & compile
COPY . .
WORKDIR "/src/BugTracker.API"
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# Publish the app
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# --- Runtime Stage ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Set environment inside container
ENV ASPNETCORE_ENVIRONMENT=Docker

# Copy published output
COPY --from=build /app/publish .

# Expose port 80 for HTTP
EXPOSE 80

# Entry point
ENTRYPOINT ["dotnet", "BugTracker.API.dll"]
