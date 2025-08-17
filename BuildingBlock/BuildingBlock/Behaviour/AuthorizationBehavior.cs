using System.Security.Claims;
using BuildingBlock.Behaviour.Interface;
using BuildingBlock.CQRS;
using Microsoft.AspNetCore.Http;

namespace BuildingBlock.Behaviour
{
     /// <summary>
     /// MediatR pipeline behavior for handling authorization before processing a command.
     /// </summary>
     /// <typeparam name="TRequest">Request type that must implement ICommand</typeparam>
     /// <typeparam name="TResponse">Response type</typeparam>
     public class AuthorizationBehavior<TRequest, TResponse>(
         IAuthorizationService<TRequest> authorizationService,
         IHttpContextAccessor httpContextAccessor)
         : IPipelineBehavior<TRequest, TResponse>
      where TRequest : notnull, IRequest<TResponse>
         where TResponse : notnull
     {
          /// <summary>
          /// Handle Authorization 
          /// </summary>
          /// <param name="request"></param>
          /// <param name="next"></param>
          /// <param name="cancellationToken"></param>
          /// <returns></returns>
          /// <exception cref="UnauthorizedAccessException"></exception>
          public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
          {
               // Check if request needs authorization
               if (request is not IRequireAuthorization)
               {
                    await authorizationService.Authorize(request, cancellationToken);
                    // Skip auth and continue
                    return await next();
               }
               await authorizationService.Authorize(request, cancellationToken);
               if (httpContextAccessor.HttpContext == null)
               {
                    throw new UnauthorizedAccessException("HttpContext is null.");
               }
               // Get the current user's claims principal]
               var user = httpContextAccessor.HttpContext?.User;
               var currentUserIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
               if (user == null || !user.Identity?.IsAuthenticated == true)
               {
                    throw new UnauthorizedAccessException("User is not authenticated.");
               }
               // Dynamically resolve the custom IAuthorizationService<TRequest>

               // Perform policy-based authorization for the request object
               //var authResult = await authorizationService.AuthorizeAsync(user, request, "DefaultPolicy");

               //if (!authResult.Succeeded)
               //{
               //     throw new UnauthorizedAccessException("User is not authorized to perform this operation.");
               //}

               // Proceed to next pipeline or handler
               return await next();
          }
     }
}
