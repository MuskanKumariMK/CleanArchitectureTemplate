using System.Reflection;
using BuildingBlock.Behaviour;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Template.Application
{
     public static class DependencyInjection
     {
          /// <summary>
          /// Registers application services for the Security application layer.
          /// </summary>
          /// <param name="service"></param>
          /// <param name="configuration"></param>
          /// <returns></returns>
          public static IServiceCollection AddApplicationService(this IServiceCollection service, IConfiguration configuration)
          {
               service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

               // MediatR behaviors
               service.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
               service.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
               service.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
               service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
               return service;
          }
     }
}
