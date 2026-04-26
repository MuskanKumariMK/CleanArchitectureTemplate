using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Template.Infrastructure.Data.Extensions
{
     /// <summary>
     /// Provides extension methods for initializing and seeding the database.
     /// </summary>
     public static class DatabaseExtension
     {
          /// <summary>
          /// Applies pending migrations only.
          /// </summary>
          public static async Task AddMigrationAsync(this IServiceProvider serviceProvider)
          {
               // Create a scope to resolve the ApplicationDbContext
               using var scope = serviceProvider.CreateScope();
               // Get the ApplicationDbContext from the service provider
               var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
               // Check if any pending Migration 
               if ((await context.Database.GetPendingMigrationsAsync()).Any())
               {
                    // Apply migrations asynchronously
                    await context.Database.MigrateAsync();
               }
          }
     }
}
