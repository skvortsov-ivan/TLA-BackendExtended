# TLA‑BackendExtended
An ASP.NET Core Web API built as a practical environment for learning and applying modern backend development techniques.  
The project focuses on clean architecture, validation, caching and external API integration with both JWT bearer token and API key.

![CI Status](https://github.com/skvortsov-ivan/TLA-BackendExtended/blob/dev/.github/workflows/ci.yml/badge.svg)

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

## Arkitektur och Systemstabilitet

### Custom Exception Middleware
För att säkerställa en enhetlig och professionell felhantering använder projektet ett anpassat **Exception Middleware**. Istället för att exponera känsliga stack traces, fångar detta middleware upp alla ohanterade undantag globalt och mappar dem till standardiserade **Problem Details** (enligt RFC 7807). Detta innebär att klienten alltid får ett konsekvent svar med korrekt HTTP-statuskod (t.ex. 404 för saknat innehåll eller 502/504 för externa tjänstefel) oavsett var i applikationen felet uppstår.

### Resilience Patterns
Applikationen kommunicerar med externa API:er (såsom AI-tjänster och kaloriberäkning) via HTTP-klienter som är förstärkta med en **Standard Resilience Handler**. Genom att integrera resiliensmönster hanteras fel proaktivt:
* **Retry Policy:** Försöker automatiskt igen vid temporära nätverksfel.
* **Circuit Breaker:** Bryter anropen tillfälligt om en extern tjänst ligger nere för att spara resurser och förhindra "cascading failures".
* **Timeout:** Säkerställer att systemet inte hänger sig i väntan på svar som dröjer för länge.

### Avancerad Caching med HybridCache
För att maximera prestanda och minimera onödiga externa API-anrop används .NET:s nya **HybridCache**. Denna lösning fungerar som ett intelligent lager mellan applikationen och datakällorna:
* **Cache-aside mönster:** Vid en förfrågan kontrolleras först cachen. Vid en "Cache Miss" hämtas data från källan och sparas sedan för framtida bruk.
* **Hybrid-ansats:** Kombinerar snabbheten i L1-cache (in-memory) med robustheten i L2-cache (distribuerad), vilket minskar latens avsevärt vid återkommande anrop.
* **Prestandamonitorering:** Systemet loggar svarstider för att påvisa prestandavinsten mellan en "Cache Hit" och en "Cache Miss".
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

