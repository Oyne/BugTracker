# Stage 1: Build the WASM app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the UI project file & restore (speedy layers)
COPY ["BugTracker.UI/BugTracker.UI.csproj", "BugTracker.UI/"]
RUN dotnet restore "BugTracker.UI/BugTracker.UI.csproj"

# Copy the rest of the source and publish
COPY . .

# Copy Docker-specific appsettings
RUN cp BugTracker.UI/wwwroot/appsettings.Docker.json BugTracker.UI/wwwroot/appsettings.json

WORKDIR "/src/BugTracker.UI"
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Serve the published files with nginx
FROM nginx:alpine AS runtime

# Clear default nginx content
RUN rm -rf /usr/share/nginx/html/*

# Copy your Blazor WebAssembly app into nginx
COPY --from=build /app/publish/wwwroot/wwwroot/. /usr/share/nginx/html/

# nginx config
RUN echo 'server { \
    listen 80; \
    root /usr/share/nginx/html; \
    index index.html index.htm; \
    location / { try_files $uri $uri/ /index.html; } \
}' > /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
