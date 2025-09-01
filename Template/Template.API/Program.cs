using Template.API;
using Template.Application;
using Template.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddApplicationService(builder.Configuration)
    .AddAPIService(builder.Configuration)
    .AddInfrastructureService(builder.Configuration);
builder.Services.AddHttpContextAccessor();
var app = builder.Build();
// ----------------------------------------------------------------------
// DATABASE MIGRATIONS (Optional but Recommended)
// ----------------------------------------------------------------------
// The line below executes a helper extension (e.g., AddMigrationAsync())
// that applies any pending Entity Framework Core migrations automatically
// when the application starts.
//
//  PROS (When Enabled):
//   - Ensures that the database schema is always in sync with the latest code.
//   - Eliminates the need to manually run "dotnet ef database update" 
//     during local development or CI/CD pipelines.where
//   - Useful in containerized deployments (Docker/Kubernetes)  
//     migrations can run on startup without extra scripting.
//
//  CONS / WARNINGS:
//   - Running migrations at startup in PRODUCTION can be risky:
//       • Potential downtime if migrations are large.
//       • May lead to race conditions if multiple instances run migrations 
//         simultaneously (use a migration gate/lock in clustered environments).
//   - Recommended approach is:
//       • Enable automatic migrations only in DEV and TEST environments.
//       • Run migrations manually or via CI/CD pipelines in PROD.
//
//  HOW TO USE:
//   1. Ensure you have a migration added via:
//         dotnet ef migrations add InitialCreate -p Infrastructure -s Api
//   2. Uncomment the line below to auto-apply pending migrations.
//   3. Place this code *before* the app starts serving requests.

// await app.Services.AddMigrationAsync(); 


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
     app.UseSwagger();
     app.UseSwaggerUI();
}
app.UseAPIService();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
