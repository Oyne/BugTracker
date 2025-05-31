# BugTracker - Running Locally with Docker

This project includes a Blazor WebAssembly UI, an ASP.NET Core API, and a SQL Server database.

## Prerequisites

- Docker installed on your machine  
- Docker Compose (usually included with Docker Desktop)

## How to run

1. Clone this repository:

    git clone [https://github.com/yourusername/your-repo.git](https://github.com/Oyne/BugTracker)
    cd your-repo

2. Build and start the containers using Docker Compose:

    docker-compose up --build

    This will build the UI and API images, start the SQL Server container, and run all services together.

3. Access the application in your browser at:

    http://localhost:5000/

    The UI will communicate with the API at `http://localhost:5000/api` inside the Docker network.

4. Application default user:
Email: admin@mail.com
Username: Admin
Password = admin1111

5. 
## Stopping/Removing the containers

To stop containers run:
docker-compose stop

To remove containers run:
docker-compose down
