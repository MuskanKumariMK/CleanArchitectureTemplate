using BuildingBlock.Exceptions.Handler;

namespace Template.API
{
     public static class DependencyInjection
     {
          public static IServiceCollection AddAPIService(this IServiceCollection service, IConfiguration configuration)
          {
               service.AddExceptionHandler<CustomExceptionHandler>();
               return service;
          }
          public static WebApplication UseAPIService(this WebApplication app)
          {
               app.UseExceptionHandler(options => { });
               app.UseAuthentication();
               app.UseAuthorization();
               return app;
          }
     }
}

