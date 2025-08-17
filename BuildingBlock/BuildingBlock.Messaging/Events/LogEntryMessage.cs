
namespace BuildingBlock.Messaging.Events
{
     public class LogEntryMessage
     {
          public DateTime Timestamp { get; set; }
          public string Level { get; set; }
          public string Message { get; set; }
          public string? Exception { get; set; }
          public string MachineName { get; set; }
          public string ServiceName { get; set; }
          public string ControllerName { get; set; }
          public string CorrelationId { get; set; }
          public string RequestPath { get; set; }
          public string HttpMethod { get; set; }
          public string UserId { get; set; }
          public string SourceIP { get; set; }
     }
}
