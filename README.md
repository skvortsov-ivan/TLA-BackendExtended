# TLA‑BackendExtended
An ASP.NET Core Web API built as a practical environment for learning and applying modern backend development techniques.  
The project focuses on clean architecture, validation, external API integration, caching, and developer‑friendly tooling.

## Overview
TLA‑BackendExtended is a backend‑only project created to practice real‑world API development patterns.  
It includes REST‑style endpoint design, layered architecture, validation, external API calls, caching, and Swagger documentation.

The API is structured to be extended over time and serves as a foundation for experimenting with backend concepts such as service abstraction, dependency injection, and performance improvements.

## Features
The project provides a clean REST API structure with controllers, services, and DTO‑based communication.  
It includes centralized error handling using ProblemDetails, custom validation filters, and a consistent request‑processing pipeline.  
External API integration is supported through Typed HttpClient, allowing the API to communicate with third‑party services in a testable and maintainable way.  
Caching mechanisms are included to reduce repeated external calls and improve performance.  
Swagger UI is available for interactive API exploration and documentation.

## Tech Stack
The API is built with ASP.NET Core and uses Swagger for documentation.  
Typed HttpClient is used for external API communication.  
Dependency Injection is used throughout the project to ensure modularity and testability.  
In‑memory caching is used to improve performance and reduce latency.  
ProblemDetails is used to provide standardized error responses.

## Architecture
The project follows a layered architecture.  
Controllers handle HTTP requests and responses.  
Services contain business logic.  
Repositories or clients abstract data access and external API calls.  
DTOs define the shape of incoming and outgoing data.  
Filters provide validation and cross‑cutting behavior.  
This structure keeps the codebase clean, maintainable, and easy to extend.

## User Secrets Setup
If the project integrates with external APIs, you may need to configure API keys using the .NET user‑secrets system.  
If authentication is added, a JWT signing key may also be required.

## Installation
Clone the repository, restore dependencies, and run the application.

git clone https://github.com/skvortsov-ivan/TLA-BackendExtended.git (github.com in Bing)  
cd TLA-BackendExtended  
dotnet restore  
dotnet run  

Example of setting secrets (adjust based on your integrations):

Right click on the solutions and click "Manage User Secrets"

TLA-BackendExtendedProxy user secrets: 
```
{
  "ApiNinjas": {
    "ApiKey": "YOUR API KEY"
  },
  "ServiceCommunicationApiKey": "jnuh78hgasjfkhjadsfhjkh38274238948239u2hdui23hfu23bu2hg3rbf8h34fh348fh7",
  "OllamaApiKey": "YOUR API KEY"
}
```

TLA-BackendExtended user secrets:
```
{
  "ServiceCommunicationApiKey": "jnuh78hgasjfkhjadsfhjkh38274238948239u2hdui23hfu23bu2hg3rbf8h34fh348fh7"
}
```

