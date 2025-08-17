using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Template.Application.Interface;

namespace Template.Infrastructure.Services
{
     /// <summary>
     /// KafkaLoggerProvider is an implementation of ILoggerProvider that creates KafkaLogger instances.
     /// </summary>
     public class KafkaLoggerProvider : ILoggerProvider
     {
          /// <summary>
          /// The producer service used to send log messages to Kafka.
          /// </summary>
          private readonly IProducerServices _producer;
          private readonly IHttpContextAccessor _httpContextAccessor;
          /// <summary>
          /// Initializes a new instance of the KafkaLoggerProvider class with the specified producer service.
          /// </summary>
          /// <param name="producer"></param>
          public KafkaLoggerProvider(IProducerServices producer, IHttpContextAccessor httpContextAccessor)
          {
               _producer = producer;
               _httpContextAccessor = httpContextAccessor;
          }
          /// <summary>
          /// Creates a logger instance for the specified category name.
          /// </summary>
          /// <param name="categoryName"></param>
          /// <returns></returns>
          public ILogger CreateLogger(string categoryName)
          {
               return new KafkaLogger(categoryName, _producer, _httpContextAccessor);
          }
          /// <summary>
          /// Disposes the KafkaLoggerProvider and releases any resources it holds.
          /// </summary>
          public void Dispose() { }
     }
}
