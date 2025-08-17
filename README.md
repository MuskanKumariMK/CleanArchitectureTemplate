![NuGet](https://img.shields.io/nuget/v/CleanArchitecture.Template.Microservices?label=NuGet)
[![GitHub](https://img.shields.io/badge/GitHub-CleanArchitectureTemplate-181717?logo=github)](https://github.com/MuskanKumariMK/TemplateProject)

# Clean Architecture Microservices Template

A **.NET 8 Clean Architecture Microservices Solution Template** designed to help you quickly scaffold modular, testable, and scalable microservices.

---

## Features

- **Clean Architecture principles** (Domain, Application, Infrastructure, API layers).
- **Microservices-ready** solution structure.
- **Dependency Injection** configured by default with helper classes for easy service registration.
- **Entity Framework Core** setup with migrations support.
- **Docker-ready** for containerized deployment.
- **Unit Tests and Integration Tests projects** included (`BuildingBlock.Tests` & `BuildingBlock.IntegrationTests`).
- **Helper utilities for tests**:
  - `TestAssertions` – simplified assertions.
  - `TestServiceProvider` – minimal DI container for tests.
  - `MockFactory` – quick creation of Moq mocks.
  - `TestLogger` – lightweight logger for testing.
  - `TestDataSeeder` – reusable generic test data generator.
- **MediatR pipeline behaviors** included for logging, validation, and authorization.
- **Architecture tests** using `NetArchTest.Rules` and `FluentAssertions` to enforce layer dependencies.
- Follows **best practices** for maintainability and scalability.

---

## Folder Structure

```bash
├── .template.config
├── BuildingBlock
│ ├── BuildingBlock
│ │ ├── Behaviour/Interface
│ │ ├── CQRS
│ │ ├── Exceptions/Handler
│ │ └── Pagination
│ ├── BuildingBlock.Messaging
│ │ ├── Consumer
│ │ ├── Events
│ │ └── Producer
│ └── BuildingBlock.Tests
│ ├── Base
│ ├── Helper
├── Template
│ ├── Template.API
│ │ ├── Controllers
│ │ └── Properties
│ ├── Template.Application
│ │ ├── Common/Exceptions
│ │ ├── DTO/Logs, Request, Response
│ │ ├── Interface
│ │ ├── Mapping
│ │ └── Restaurant
│ ├── Template.Domain
│ └── Template.Infrastructure
│ ├── Data/Extensions, Seed
│ ├── Repository
│ └── Services
└── Template.Tests
├── Template.Application.UnitTests
├── Template.Architecture.Tests
├── Template.Domain.UnitTests
└── Template.Integration.Tests
```

- `BuildingBlock` – reusable building blocks (Behaviour, CQRS, Exceptions, Pagination).
- `BuildingBlock.Messaging` – consumer, producer, and events infrastructure.
- `BuildingBlock.Tests` – unit and integration test helpers.
- `Template.API` – entry point for your API.
- `Template.Application` – application layer with DTOs, interfaces, and business logic.
- `Template.Domain` – domain entities and models.
- `Template.Infrastructure` – database, repository, and services implementations.
- `Template.Tests` – unit, integration, and architecture tests.

---

## Installation

Install the template globally from NuGet:

```bash
dotnet new install CleanArchitecture.Template.Microservices
```

Create a new project from the template:

```bash
dotnet new clean-arch-microservice -n MyMicroservice
```

```bash
cd MyMicroservice
```

---

## Running Tests

Unit tests and integration tests are already configured with xUnit and Moq.

Run all tests with:

```bash
dotnet test
```

## Components

### TestAssertions

```csharp
Using Helpers

// Simplify assertions:

TestAssertions.ShouldBeEmpty(new List<int>());
TestAssertions.ShouldContain(users, users[0]);
```

---

### TestServiceProvider

```csharp

// Build a minimal DI container for tests:

var provider = TestServiceProvider.Build(services =>
{
    services.AddScoped<IMyService, MyService>();
});
var myService = provider.GetRequiredService<IMyService>();
```

---

### MockFactory

```csharp

// Quickly create Moq mocks:

var mockRepo = MockFactory.CreateMock<IRepository>();
mockRepo.Setup(x => x.GetAll()).Returns(new List<Entity>());
```

---

### TestDataSeeder

```csharp

// Generate reusable sample data:

var users = TestDataSeeder.Seed(5, i => new User
{
    Id = i,
    Name = $"User {i}",
    Email = $"user{i}@example.com"
});
```

---

### TestLogger

```csharp

// Lightweight logger for testing:

var logger = TestLogger.CreateLogger<MyService>();
logger.LogInformation("Test log message");
```

---

## MediatR Pipeline Behaviors

### AuthorizationBehavior

```csharp
// Handles authorization before processing a request:

var authBehavior = new AuthorizationBehavior<MyCommand, MyResponse>(authorizationService, httpContextAccessor);
```

---

### LoggingBehaviour

```csharp
// Logs requests and measures execution time:

var loggingBehavior = new LoggingBehaviour<MyCommand, MyResponse>(logger, loggerService);
```

---

### ValidationBehaviour

```csharp
// Validates requests using FluentValidation before processing:

var validationBehavior = new ValidationBehaviour<MyCommand, MyResponse>(validators);
```

---

### CQRS Interfaces

```csharp
Commands
public interface ICommand : ICommand<Unit> { }
public interface ICommand<out TResponse> : IRequest<TResponse> where TResponse : notnull { }
```

---

```csharp
Command Handlers
public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit> where TCommand : ICommand<Unit> { }
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
where TCommand : ICommand<TResponse>
where TResponse : notnull { }
```

---

```csharp
Queries
public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull { }
```

---

```csharp
Authorization & Logging
public interface IAuthorizationService<TRequest>
{
Task Authorize(TRequest request, CancellationToken cancellationToken);
}

public interface ILoggerService<TRequest>
{
Task Log(TRequest request, string result);
}
```

---

```csharp
//Marker Interface
public interface IRequireAuthorization { }
```

---

### Pagination

```csharp
Generic pagination class:

public class PaginateResult<TEntity>(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
where TEntity : class
{
public int PageIndex { get; } = pageIndex;
public int PageSize { get; } = pageSize;
public IEnumerable<TEntity> Data { get; } = data;
public long Count { get; } = count;
}
```

---

### Exception Handling

```csharp

// Custom exception handler for consistent API responses:

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
app.UseExceptionHandler();
```

Supports:

```csharp
InternalServerException → 500

ValidationException → 400

BadRequestException → 400

ConflictException → 409

NotFoundException → 404
```

- Returns ProblemDetails with trace identifiers and validation errors.

---

## Architecture Enforcement

- Your template includes layered architecture tests:

- Domain layer must not depend on other layers.

- Application layer depends only on Domain.

- Infrastructure depends on Domain/Application only.

- API layer depends on Application and MediatR, but not on Infrastructure.

- Run these tests using dotnet test to enforce architectural rules.
