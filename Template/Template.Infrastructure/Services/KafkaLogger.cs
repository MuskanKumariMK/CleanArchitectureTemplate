using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Restaurant.Application.DTO.Logs;
using Template.Application.Interface;

namespace Template.Infrastructure.Services
{
     /// <summary>
     /// KafkaLogger is an implementation of ILogger that sends log messages to a Kafka topic.
     /// </summary>
     public class KafkaLogger : ILogger
     {
          /// <summary>
          /// Initializes a new instance of the KafkaLogger class with the specified category name and producer.
          /// </summary>
          private readonly string _categoryName;
          private readonly IProducerServices _producer;
          private const string TopicName = "log-entry-topic";
          private readonly IHttpContextAccessor _httpContextAccessor;
          /// <summary>
          /// Creates a new instance of the KafkaLogger class.
          /// </summary>
          /// <param name="categoryName"></param>
          /// <param name="producer"></param>
          public KafkaLogger(string categoryName, IProducerServices producer, IHttpContextAccessor httpContextAccessor)
          {
               _categoryName = categoryName;
               _producer = producer;
               _httpContextAccessor = httpContextAccessor;
          }
          /// <summary>
          /// Begins a logical operation scope with the specified state.
          /// <Details>
          /// 
          /// </Details>
          /// </summary>
          /// <typeparam name="TState"></typeparam>
          /// <param name="state"></param>
          /// <returns></returns>
          public IDisposable BeginScope<TState>(TState state) => default!;
          /// <summary>
          /// Checks if the logger is enabled for the specified log level.
          /// </summary>
          /// <param name="logLevel"></param>
          /// <returns></returns>
          public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;
          /// <summary>
          /// Logs a message with the specified log level, event ID, state, exception, and formatter.
          /// </summary>
          /// <typeparam name="TState"></typeparam>
          /// <param name="logLevel"></param>
          /// <param name="eventId"></param>
          /// <param name="state"></param>
          /// <param name="exception"></param>
          /// <param name="formatter"></param>
          public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
              Func<TState, Exception?, string> formatter)
          {
               // Check if the log level is enabled before proceeding
               if (!IsEnabled(logLevel)) return;
               // Create a log entry object with the necessary information
               var context = _httpContextAccessor.HttpContext;
               var log = new LogEntryMessage
               {
                    Timestamp = DateTime.UtcNow,
                    Level = logLevel.ToString(),
                    Message = formatter(state, exception),
                    ServiceName = "Security ",
                    Exception = exception?.ToString() ?? "No exception thrown",
                    ControllerName = _categoryName,
                    CorrelationId = Guid.NewGuid().ToString(),
                    MachineName = Environment.MachineName,
                    HttpMethod = context?.Request?.Method ?? "N/A",
                    RequestPath = context?.Request?.Path.Value?.ToString() ?? "N/A",
                    UserId = context?.User?.Identity?.Name ?? "anonymous",
                    SourceIP = context?.Connection?.RemoteIpAddress?.ToString() ?? "unknown"
               };

               //var log = LogEntryFactory.Create(logLevel.ToString(), formatter(state, exception), _categoryName, _httpContextAccessor, exception);

               // Serialize the log entry to JSON and send it to the Kafka topic
               var jsonMessage = JsonSerializer.Serialize(log);
               // Use the producer to send the log message to the Kafka topic
               //_producer.ProduceAsync(TopicName, jsonMessage).ContinueWith(task =>
               //{
               //     if (task.IsFaulted)
               //     {
               //          // Optional: write to a fallback log file or alert system
               //          Console.WriteLine($"Kafka logging failed: {task.Exception?.GetBaseException().Message}");
               //     }
               //});  // Fire and forget
               try
               {
                    _producer.ProduceAsync(TopicName, jsonMessage);
               }
               catch (Exception ex)
               {
                    Console.WriteLine($"Kafka log failed: {ex.Message}");
               }

          }
     }

}
