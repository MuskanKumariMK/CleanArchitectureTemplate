using System.Diagnostics;
using BuildingBlock.Behaviour.Interface;
using Microsoft.Extensions.Logging;

namespace BuildingBlock.Behaviour
{
     /// <summary>
     /// Creating mediatr pipeline behaviour for logging, which executes before and after a the request is handled.
     /// </summary>
     /// <typeparam name="TRequest"></typeparam>
     /// The request type being handled by the pipeline
     /// <typeparam name="TResponse"></typeparam>
     /// The response type being handled by the pipeline
     /// <param name="logger"></param>
     /// ILogger instance to log the request and response
     public class LoggingBehaviour<TRequest, TResponse>
         //Use constructor injection to get an ILogger instance  
         (ILogger<LoggingBehaviour<TRequest, TResponse>> logger, ILoggerService<TRequest> _loggerService)
           : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
         where TResponse : notnull

     {
          /// <summary>
          /// This method calls before and after MediatR handles a request
          /// It log request processing and measure execution time
          /// </summary>
          /// <param name="request"></param>
          /// <param name="next"></param>
          /// <param name="cancellationToken"></param>
          /// <returns></returns>
          public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
          {
               await _loggerService.Log(request, "Request processing started.");
               //Login before exection of the request
               logger.LogInformation("[START] Handle request={Request} - Response={Response} - RequestData={RequestData}",
               typeof(TRequest).Name, typeof(TResponse).Name, request);
               //Measure the time taken to execute the request
               var timer = new Stopwatch();
               timer.Start();
               //Call the next delegate to handle the request
               var response = await next();

               timer.Stop();
               var timeTaken = timer.Elapsed;
               if (timeTaken.Seconds > 3) // if the request is greater than 3 seconds, then log the warnings
               {
                    logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken} seconds.",
                        typeof(TRequest).Name, timeTaken.Seconds);
               }
               logger.LogInformation("[END] Handled {Request} with {Response}", typeof(TRequest).Name, typeof(TResponse).Name);
               await _loggerService.Log(request, "Request processed successfully.");
               return response;

          }
     }
}
