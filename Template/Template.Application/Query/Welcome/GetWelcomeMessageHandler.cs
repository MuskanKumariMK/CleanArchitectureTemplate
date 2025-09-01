using MediatR;

namespace Template.Application.Query.Welcome
{
     /// <summary>
     /// Handler for the GetWelcomeMessageQuery
     /// </summary>
     public class GetWelcomeMessageHandler : IRequestHandler<GetWelcomeMessageQuery, GetWelcomeMessageResult>
     {
          public Task<GetWelcomeMessageResult> Handle(GetWelcomeMessageQuery request, CancellationToken cancellationToken)
          {
               var result = new GetWelcomeMessageResult("Welcome to the Clean Architecture Microservices Template!");
               return Task.FromResult(result);
          }
     }
}
