# Petshop REST API

Welcome to the Petshop REST API, a project built on .NET Core. This API serves as the backend for managing various aspects of a pet store. Please note that due to certain compatibility issues and time constraints, the full functionality of the project has not been completed. However, I am committed to rectifying these shortcomings should the opportunity arise.

## Project Structure

The project follows a structured format with the following components:

### DTOs (Data Transfer Objects)

These objects facilitate the transfer of data between the API and the client, ensuring efficient communication.

### Controllers

Controllers handle the incoming HTTP requests and manage the workflow of the API, allowing for the execution of various operations.

### Repositories

Repositories are responsible for managing data storage and retrieval from the underlying database, ensuring smooth interactions between the API and the data source.

### Entities

Entities represent the various data models and structures used within the application, defining the core components of the database schema.

### Configuration

The configuration files provide essential settings and parameters for the proper functioning of the API and related components.

## Incomplete Functionality

Regrettably, the project couldn't achieve its full potential due to certain compatibility issues and time constraints. Despite this setback, I am eager and prepared to invest the necessary effort and time to enhance the functionality and ensure the completion of this project.

## Installation

To use this API, follow these steps:

1. Clone the repository to your local machine.
2. Ensure that you have the latest version of .NET Core installed.
3. Configure the necessary environment variables and database connections.
4. Run the application and access the API endpoints through the specified routes.

## Issue Encountered

Unfortunately, the current version of the API encounters an issue preventing the server from starting, and consequently, any requests to the API cannot be processed. This issue arises from a version mismatch in the dependencies related to the package "Microsoft.EntityFrameworkCore.Relational," leading to an inability to establish a functional connection with the underlying database.

![Imagen de ejemplo](/Images/ErrorScreenshot_1.png)

## Error Details

The error primarily stems from conflicting versions of the "Microsoft.EntityFrameworkCore.Relational" package. Despite attempts to resolve the issue within the given time frame, the intricate nature of the dependency conflict demands a more comprehensive investigation and resolution process.

To rectify this issue, a thorough analysis of the package's compatibility with the other components of the project is required. I am dedicated to resolving this matter promptly to ensure the seamless functionality of the API.

## Acknowledgements

A special thank you to all my fellow campus mates who diligently worked to resolve this issue, including @andersoncespedes, @Marsh1100, and @MarioDaza25.

Thank you for considering this project. Your support and understanding are deeply appreciated.
