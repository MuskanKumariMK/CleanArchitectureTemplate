[![NuGet](https://img.shields.io/nuget/v/Coreplex.CleanArchitecture.Template.Microservices?label=NuGet)](https://www.nuget.org/packages/Coreplex.CleanArchitecture.Template.Microservices)

[![GitHub](https://img.shields.io/badge/GitHub-CleanArchitectureTemplate-181717?logo=github)](https://github.com/userKumariMK/TemplateProject)

# Clean Architecture Microservices Template

A **.NET 8 Clean Architecture Microservices Solution Template** designed to help you quickly scaffold modular, testable, and scalable microservices.

---

## Features

- **Clean Architecture principles** (Domain, Application, Infrastructure, API layers).
- **Microservices-ready** solution structure.
- **Dependency Injection** configured by default with helper classes for easy service registration.
- **Entity Framework Core** setup with migrations support.
- **Docker-ready** for containerized deployment.
- **Generic Repository & Unit of Work Pattern(v2)** Centralized repository access with async CRUD operations. UnitOfWork ensures transaction consistency across multiple repositories.
- **Advanced Pagination (v2)** PaginateRequest & PaginateResult<TEntity> For paging, searching, and sorting.Dynamic sortableColumns mapping for flexible queries.
  **Domain Events (v2)** Aggregate root support with AddDomainEvent.Events implemented with IDomainEvent (MediatR INotification).Automatic dispatching of events after persistence.

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

## Version 2 Updates

This release is a **major enhancement** to the Clean Architecture Microservices Template.  
Unlike a simple patch, these updates significantly improve **flexibility, maintainability, and scalability** of the solution.

### What’s New

- **Advanced Pagination Support**

  - Added `PaginateRequest` record with page, size, search, and sorting options.
  - Updated `PaginateResult<TEntity>` with `HasNextPage`, `HasPreviousPage`, and async creation.

- **Generic Repository Enhancements**

  - New `GetPaginateAsync` method supports:
    - Filtering with `Expression<Func<TEntity, bool>>`
    - Search using delegates for custom queries
    - Dynamic sorting using `sortableColumns` dictionary
  - Provides **standardized query handling** across all repositories.

- **Unit of Work Pattern**

  - Added `UnitOfWork` implementation to manage repository transactions.
  - Ensures **transaction consistency** with a single `SaveChangeAsync()` call.
  - Keeps repositories coordinated under one `DbContext`.

- **Domain Abstractions (DDD-ready)**
  - Introduced `Entity<T>` with auditing fields (`CreatedAt`, `UpdatedAt`, etc.).
  - Added `Aggregate<TId>` for handling domain events.
  - Added `IDomainEvent` interface based on MediatR’s `INotification`.
  - Enables **event-driven workflows** in the Domain layer.

---

### How to Use the New Features

---

### How to Use the New Features

#### 1. Paginated Query with Filters & Sorting (CQRS Query Example)

```csharp
// Query
public record GetUsersQuery(int PageIndex, int PageSize, string? Search, string? SortColumn, string? SortOrder)
    : IRequest<PaginateResult<User>>;

// Handler
public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginateResult<User>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUsersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginateResult<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository<User>().GetPaginateAsync(
            new PaginateRequest(request.PageIndex, request.PageSize, request.SortColumn, request.SortOrder),
            filter: u => u.IsActive,
            searchFilter: q => q.Where(u => u.Name.Contains(request.Search ?? "")
                                         || u.Email.Contains(request.Search ?? "")),
            sortableColumns: new()
            {
                { "name", u => u.Name },
                { "email", u => u.Email }
            });
    }
}


```

---

#### 2. Using Unit of Work

```csharp

// User Add Command and handler Using UnitOfWork
// Command
public record CreateUserCommand(string Name, string Email) : IRequest<Guid>;

// Handler
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            IsActive = true
        };

        await _unitOfWork.Repository<User>().AddAsync(user);
        await _unitOfWork.SaveChangeAsync();

        return user.Id;
    }
}

```

---

#### 3. Working with Domain Events

##### Step 1 – Create the Domain Event

```csharp
// Domain Event
public record UserCreatedEvent(User User) : IDomainEvent;

```

---

##### Step 2 – Update the Aggregate Root

```csharp

// Aggregate Root
public class User : Aggregate<Guid>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }

    public static User Create(string name, string email)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            IsActive = true
        };

        user.AddDomainEvent(new UserCreatedEvent(user));
        return user;
    }
}
```

##### Step 3 – Handle the Event

```csharp
// Event Handler (MediatR)
public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
{
    public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"New user created: {notification.User.Name} ({notification.User.Email})");
        return Task.CompletedTask;
    }
}
```

##### Step 4 – Use Domain Events

```csharp
var user = User.Create("User", "user@example.com");

// Get and publish events via MediatR
var events = user.ClearDomainEvents();
foreach (var domainEvent in events)
{
    await _mediator.Publish(domainEvent);
}
```

---

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

     public record PaginateRequest(int PageIndex = 0, int PageSize = 10,string? Search = null,string? SortColumn = null, string SortOrder = "asc");
}
```

```csharp
Generic pagination class:

// Pagination Requets
  public class PaginateResult<TEntity>
    where TEntity : class
  {
       public PaginateResult(int pageIndex, int pageSize, long count, List<TEntity> data)
       {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
       }


       public int PageIndex { get; }

       public int PageSize { get; }

       public long Count { get; }

       public List<TEntity> Data { get; }

       public bool HasNextPage => (PageIndex * PageSize) < Count;

       public bool HasPreviousPage => PageIndex > 1;

       public static async Task<PaginateResult<TEntity>> CreateAsync(IQueryable<TEntity> query, int pageIndex, int pageSize)
       {
           int totalCount = await query.CountAsync();

            var data = await query
                .Skip((pageIndex - 1) * pageSize) // Skip items from previous pages
                .Take(pageSize)                  // Take the items for the current page
                .ToListAsync();

            // Return a new PaginateResult instance with the fetched data.
            return new(pageIndex, pageSize, totalCount, data);
       }
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

### Domain Abstractions

- Domain Abstractions

- IEntity & IEntity<T> → Define common properties for all entities (with auditing fields).

- Entity<T> → Abstract base entity with audit fields (CreatedAt, CreatedBy, etc.).

- Aggregate<TId> → Extends Entity<TId> and manages domain events.

- IAggregate & IAggregate<T> → Contracts for aggregates and domain event handling.

- IDomainEvent → Implements INotification (MediatR), enabling event-driven workflows.

```csharp
public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    DateTime OccurredOn => DateTime.Now;
    string EventType => GetType().AssemblyQualifiedName;
}
```

### Repository & Unit of Work

---

## Architecture Enforcement

- Your template includes layered architecture tests:

- Domain layer must not depend on other layers.

- Application layer depends only on Domain.

- Infrastructure depends on Domain/Application only.

- API layer depends on Application and MediatR, but not on Infrastructure.

- Run these tests using dotnet test to enforce architectural rules.
