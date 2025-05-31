# Stage 1: Build the WASM app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Install Node.js (LTS) and Dart Sass (globally)
RUN apt-get update && \
    apt-get install -y curl gnupg && \
    curl -fsSL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get install -y nodejs && \
    npm install -g sass && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

# Copy only the UI project file & restore (for layer caching)
COPY ["BugTracker.UI/BugTracker.UI.csproj", "BugTracker.UI/"]
RUN dotnet restore "BugTracker.UI/BugTracker.UI.csproj"

# Copy the rest of the source code
COPY . .

# Copy Docker-specific appsettings
RUN cp BugTracker.UI/wwwroot/appsettings.Docker.json BugTracker.UI/wwwroot/appsettings.json

WORKDIR "/src/BugTracker.UI"

# Build the app (Sass will be compiled by DartSassBuilder during this)
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Serve the published files with nginx
FROM nginx:alpine AS runtime

# Clear default nginx content
RUN rm -rf /usr/share/nginx/html/*

# Copy the Blazor WebAssembly app to nginx
COPY --from=build /app/publish/wwwroot/wwwroot/. /usr/share/nginx/html/

# Configure nginx to support SPA routing
RUN echo 'server { \
    listen 80; \
    root /usr/share/nginx/html; \
    index index.html index.htm; \
    location / { try_files $uri $uri/ /index.html; } \
}' > /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
