using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Tests.Helper
{

     /// <summary>
     /// Helper class to build a minimal dependency injection (DI) container for unit tests.
     /// 
     /// This allows you to register only the required services for a particular test scenario,
     /// without setting up the full application DI container.
     /// </summary>
     public static class TestServiceProvider
     {
          /// <summary>
          /// Builds a service provider with the specified service registrations.
          /// </summary>
          /// <param name="configureServices">An action to configure the <see cref="IServiceCollection"/>.</param>
          /// <returns>An <see cref="IServiceProvider"/> containing the registered services.</returns>
          /// <remarks>
          /// Example usage:
          /// <code>
          /// var provider = TestServiceProvider.Build(services =>
          /// {
          ///     services.AddScoped<IMyService, MyService>();
          /// });
          /// 
          /// var myService = provider.GetRequiredService<IMyService>();
          /// </code>
          /// </remarks>
          public static IServiceProvider Build(Action<IServiceCollection> configureServices)
          {
               if (configureServices == null) throw new ArgumentNullException(nameof(configureServices));

               // Create a new service collection
               var services = new ServiceCollection();

               // Apply the provided service registrations
               configureServices(services);

               // Build and return the service provider
               return services.BuildServiceProvider();
          }
     }
}

/* Example Usage:

// Build a minimal DI container for a test
var provider = TestServiceProvider.Build(services =>
{
    services.AddScoped<IMyService, MyService>();
});

// Resolve a service from the container
var myService = provider.GetRequiredService<IMyService>();

*/