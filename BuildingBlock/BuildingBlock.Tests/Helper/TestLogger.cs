using Microsoft.Extensions.Logging;

namespace BuildingBlock.Tests.Helper
{
     /// <summary>
     /// Provides a simple logger for unit tests.
     /// 
     /// This class allows you to inject a logger into any service under test
     /// without having to configure a full logging infrastructure.
     /// Useful for capturing log messages or avoiding null loggers in tests.
     /// </summary>
     public static class TestLogger
     {
          /// <summary>
          /// Creates a simple logger instance for the specified type <typeparamref name="T"/>.
          /// 
          /// The logger uses the Microsoft.Extensions.Logging infrastructure with minimal configuration.
          /// The log level is set to Debug by default.
          /// </summary>
          /// <typeparam name="T">The type that the logger is associated with (usually the service being tested).</typeparam>
          /// <returns>An <see cref="ILogger{T}"/> instance that can be injected into your service.</returns>
          public static ILogger<T> CreateLogger<T>()
          {
               // Create a logger factory using minimal configuration.
               // You can expand this to add console, file, or other providers if needed.
               using var factory = LoggerFactory.Create(builder =>
               {
                    // Set the minimum log level to Debug, so all messages are captured
                    builder.SetMinimumLevel(LogLevel.Debug);

                    // Optional: Add additional logging providers if needed, e.g., Console, Debug, etc.
                    // builder.AddConsole();
                    // builder.AddDebug();
               });

               // Create a logger specifically for the type T
               return factory.CreateLogger<T>();
          }
     }
}

/* Example Usage:

// Create a logger for your service
var logger = TestLogger.CreateLogger<MyService>();

// Use it in your service constructor
var service = new MyService(logger);

// Log messages during test execution
logger.LogInformation("This is a test log message");

*/