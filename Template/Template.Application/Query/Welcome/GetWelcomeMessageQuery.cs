using BuildingBlock.Behaviour.Interface;
using BuildingBlock.CQRS;

namespace Template.Application.Query.Welcome
{
     /// <summary>
     /// Query to request the welcome message.
     /// </summary
     public record GetWelcomeMessageQuery : IQuery<GetWelcomeMessageResult>;

     /// <summary>
     /// Result returned for the welcome message query.
     /// </summary>
     /// <param name="Message">The welcome message text.</param>
     public record GetWelcomeMessageResult(string result);
     public static class Welcome
     {
          public class Authorization : IAuthorizationService<GetWelcomeMessageQuery>
          {
               public Task Authorize(GetWelcomeMessageQuery request, CancellationToken cancellationToken)
               {
                    return Task.CompletedTask;
               }
          }

          public class Logger : ILoggerService<GetWelcomeMessageQuery>
          {
               public Task Log(GetWelcomeMessageQuery request, string result)
               {
                    return Task.CompletedTask;
               }
          }
     }
}