# TaskSystem

TaskSystem is a .NET 9-based application designed to manage tasks and provide a robust API for interacting with MongoDB. It includes a Web API, background services, and persistence layers, all containerized using Docker.

## Features
- **Web API**: Provides endpoints for managing movies and tasks.
- **MongoDB Integration**: Uses MongoDB for data storage.
- **Background Services**: Handles asynchronous tasks such as email notifications.
- **Dockerized**: Easily deployable using Docker and Docker Compose.
- **Swagger Integration**: API documentation and testing.

## Technologies Used
- **.NET 9**
- **MongoDB**
- **Docker**
- **AutoMapper**
- **Entity Framework Core**
- **FluentEmail**

## Prerequisites
- Docker and Docker Compose installed.
- .NET 9 SDK installed.

## Installation
1. Clone the repository:
2. Build and run the application using Docker Compose:

## Usage
- Access the Web API at `http://localhost:5000`.
- Swagger documentation is available at `http://localhost:5000/swagger`.

## Environment Variables
- `MONGO_CONNECTION_STRING`: Connection string for MongoDB.
- `MONGO_INITDB_ROOT_USERNAME`: MongoDB root username.
- `MONGO_INITDB_ROOT_PASSWORD`: MongoDB root password.

## Project Structure
- **TaskSystem.WebApi**: Contains the Web API and controllers.
- **TaskSystem.Application**: Business logic and services.
- **TaskSystem.Infrastructure**: Repository and database context.
- **TaskSystem.Persistence**: Persistence layer for Entity Framework Core.
- **TaskSystem.BackgroundServices**: Background services for task processing.

## Docker Setup
- **MongoDB**: Runs on port `27017`.
- **Web API**: Runs on port `5000`.

## Testing
Unit tests are located in the `TaskSystem.Application.Tests` project. Run tests using:

## License
This project is licensed under the MIT License.