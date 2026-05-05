# TLA‑BackendExtended
An ASP.NET Core Web API built as a practical environment for learning and applying modern backend development techniques.  
The project focuses on clean architecture, validation, caching and external API integration with both JWT bearer token and API key.

![CI](https://github.com/skvortsov-ivan/TLA-BackendExtended/actions/workflows/ci.yml/badge.svg)
![codecov](https://codecov.io/gh/skvortsov-ivan/TLA-BackendExtended/branch/dev/graph/badge.svg)


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
The API is built with ASP.NET Core and uses Scalar for documentation.  
Typed HttpClient is used for external API communication.  
Dependency Injection is used throughout the project to ensure modularity and testability.  
In‑memory caching is used to improve performance and reduce latency.  
ProblemDetails is used to provide standardized error responses.

## Architecture and System Stability

### Custom Exception Middleware
To ensure uniform and professional error handling, the project utilizes a custom **Exception Middleware**. Instead of exposing sensitive stack traces, this middleware intercepts all unhandled exceptions globally and maps them to standardized **Problem Details** (in accordance with RFC 7807). 

This ensures that the client always receives a consistent response with the appropriate HTTP status code—such as **404** for missing content or **502/504** for external service failures—regardless of where the error occurs within the application.

---

### Resilience Patterns
The application communicates with external APIs (such as AI services and calorie calculation engines) via HTTP clients hardened with a **Standard Resilience Handler**. By integrating resilience patterns, the system manages potential failures proactively:

* **Retry Policy:** Automatically retries requests during transient network errors.
* **Circuit Breaker:** Temporarily halts requests if an external service is down to conserve resources and prevent "cascading failures."
* **Timeout:** Ensures the system does not hang indefinitely while waiting for a delayed response.

---

### Advanced Caching with HybridCache
To maximize performance and minimize unnecessary external API calls, the system implements .NET’s new **HybridCache**. This solution acts as an intelligent layer between the application and its data sources:

* **Cache-aside Pattern:** Upon a request, the system first checks the cache. On a "Cache Miss," data is fetched from the source and then stored for future use.
* **Hybrid Approach:** Combines the lightning speed of **L1 caching** (in-memory) with the robustness of **L2 caching** (distributed), significantly reducing latency for recurring requests.
* **Performance Monitoring:** The system logs response times to demonstrate the performance gains achieved between a "Cache Hit" and a "Cache Miss."
  
## User Secrets Setup
The project integrates with external APIs, you may need to configure API keys using the .NET user‑secrets system.  
If authentication is added, a JWT signing key may also be required.

## Installation
The project integrates with external APIs, you need to configure API keys using the .NET user‑secrets system.  
If authentication is added, a JWT signing key is also required.

Clone the repository, restore dependencies, and run the application.

```
git clone https://github.com/skvortsov-ivan/TLA-BackendExtended.git
cd TLA-BackendExtended  
dotnet restore  
dotnet run  
```

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

