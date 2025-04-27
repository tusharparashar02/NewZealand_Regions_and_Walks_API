# New Zealand Regions and Walks API

This is a RESTful API built with ASP.NET Core Web API (using .NET 8) that provides information about regions and walks in New Zealand. The API allows users to perform CRUD operations on regions and walks, with additional functionality like filtering, sorting, pagination, authentication, and role-based authorization.

## Features

- **CRUD Operations**: Create, Read, Update, and Delete regions and walks information.
- **Authentication**: Secure API endpoints using JWT tokens for authenticated access.
- **Role-Based Authorization**: Define user roles (Admin, User) with different access permissions.
- **Filtering, Sorting, and Pagination**: Filter, sort, and paginate results for easier data retrieval.
- **Validation**: Validate inputs to ensure data integrity.
- **Swagger**: Use Swagger UI for easy testing and documentation of the API.
- **Asynchronous Programming**: Implement asynchronous calls for better performance.
- **Logging and Exception Handling**: Log application behavior and handle exceptions gracefully.
- **Image Upload**: Ability to upload images for regions and walks, allowing users to associate images with the respective data.

## Technologies Used

- **ASP.NET Core 8**: The latest version of ASP.NET Core for building Web APIs.
- **Entity Framework Core**: For database interaction and ORM.
- **SQL Server**: Relational database for storing regions and walks data.
- **JWT Authentication**: Secure endpoints and manage user access.
- **AutoMapper**: For mapping domain models to Data Transfer Objects (DTOs).
- **Swagger**: For documenting and testing the API endpoints.
- **Dependency Injection**: Built-in support for dependency injection in ASP.NET Core.

## Setup and Installation

Follow the steps below to get your local environment up and running:

### Prerequisites

- **.NET 8 SDK**: Install the latest version of .NET SDK from the [official .NET website](https://dotnet.microsoft.com/download/dotnet).
- **SQL Server**: Have SQL Server installed or use a cloud database like Azure SQL Database.
- **Visual Studio Code or Visual Studio**: You can use any IDE for .NET development, but Visual Studio Code is recommended for this project.

### Steps

1. Clone the repository:

    ```bash
    git clone https://github.com/yourusername/new-zealand-regions-walks-api.git
    cd new-zealand-regions-walks-api
    ```

2. Open the solution in your preferred IDE (e.g., Visual Studio or Visual Studio Code).

3. Update the `appsettings.json` file with your SQL Server connection string.

4. Run the following commands to apply Entity Framework Core migrations:

    ```bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

5. Start the application:

    ```bash
    dotnet run
    ```

    The API will be running on `https://localhost:5001` by default.

## API Endpoints

### Regions

- **GET** `/api/regions`: Get a list of all regions.
- **GET** `/api/regions/{id}`: Get a specific region by ID.
- **POST** `/api/regions`: Create a new region.
- **PUT** `/api/regions/{id}`: Update an existing region.
- **DELETE** `/api/regions/{id}`: Delete a region.
- **POST** `/api/regions/{id}/upload-image`: Upload an image for a region.

### Walks

- **GET** `/api/walks`: Get a list of all walks.
- **GET** `/api/walks/{id}`: Get a specific walk by ID.
- **POST** `/api/walks`: Create a new walk.
- **PUT** `/api/walks/{id}`: Update an existing walk.
- **DELETE** `/api/walks/{id}`: Delete a walk.
- **POST** `/api/walks/{id}/upload-image`: Upload an image for a walk.

### Authentication

- **POST** `/api/auth/login`: Login and get a JWT token.
- **POST** `/api/auth/register`: Register a new user.

### Swagger UI

You can access the Swagger UI at:

