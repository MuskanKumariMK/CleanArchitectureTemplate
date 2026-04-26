using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Infrastructure.Data;
namespace Template.Infrastructure
{
     public static class DependencyInjection
     {
          /// <summary>  
          ///  
          /// </summary>  
          /// <param name="service"></param>  
          /// <param name="configuration"></param>  
          /// <returns></returns>  
          public static IServiceCollection AddInfrastructureService(this IServiceCollection service, IConfiguration configuration)
          {
               var connectionString = configuration.GetConnectionString("TemplateDb");
               // Register DbContext  
               service.AddDbContext<ApplicationDbContext>((sp, options) =>
               {
                    // Sql Server  
                    options.UseSqlServer(connectionString);
               });

               service.AddHttpClient();

               return service;
          }
     }
}
