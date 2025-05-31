# BugTracker - Running Locally with Docker

This project includes a Blazor WebAssembly UI, an ASP.NET Core API, and a SQL Server database.

## Prerequisites

- Docker installed on your machine (If not you can do it [here](https://www.docker.com/get-started/))
- Docker Compose (usually included with Docker Desktop)

## How to run

1. Clone this repository:

    git clone https://github.com/Oyne/BugTracker
    
    cd path to cloned repository

3. Build and start the containers using Docker Compose:

    docker compose up --build

    This will build the UI and API images, start the SQL Server container, and run all services together.

4. Access the application in your browser at:

    http://localhost:5001/

    The UI will communicate with the API at `http://localhost:5000/api` inside the Docker network.

5. Application default user:

    Email: admin@mail.com

    Username: Admin

    Password = admin1111
 
## Stopping/Removing the containers

To stop containers run:

docker compose stop

To remove containers run:

docker compose down

## Updating Your Local Repository

To pull changes run: 

git pull

After pulling changes check if there were changes to Dockerfiles or docker.compose.yml, if yes then run:

docker compose down

docker compose up --build

If no just rebuild:

docker compose up --build



