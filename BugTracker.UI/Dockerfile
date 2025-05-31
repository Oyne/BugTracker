# Stage 1: Build the WASM app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Install Node.js (LTS) and Dart Sass globally
RUN apt-get update && \
    apt-get install -y curl && \
    curl -fsSL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get install -y nodejs && \
    npm install -g sass && \
    apt-get clean && rm -rf /var/lib/apt/lists/*

# Copy csproj and restore as separate layers for better cache
COPY ["BugTracker.UI/BugTracker.UI.csproj", "BugTracker.UI/"]
COPY ["BugTracker.Shared/BugTracker.Shared.csproj", "BugTracker.Shared/"]
RUN dotnet restore "BugTracker.UI/BugTracker.UI.csproj"

# Copy everything else
COPY . .

# Set working directory to UI project
WORKDIR /app/BugTracker.UI

# Ensure Docker-specific appsettings are used
RUN cp wwwroot/appsettings.Docker.json wwwroot/appsettings.json

# Build
RUN dotnet build -c Release

# Publish 
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Serve with nginx
FROM nginx:alpine AS runtime

# Remove default nginx static content
RUN rm -rf /usr/share/nginx/html/*

# Copy published files
COPY --from=build /app/publish/wwwroot/wwwroot/. /usr/share/nginx/html/

# Setup nginx for SPA fallback routing
RUN echo 'server { \
    listen 80; \
    root /usr/share/nginx/html; \
    index index.html index.htm; \
    location / { try_files $uri $uri/ /index.html; } \
}' > /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
