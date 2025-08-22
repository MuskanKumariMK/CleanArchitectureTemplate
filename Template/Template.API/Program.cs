using Template.API;
using Template.Application;
using Template.Infrastructure;
using Template.Infrastructure.Data.Extensions;

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
await app.Services.AddMigrationAsync();
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
