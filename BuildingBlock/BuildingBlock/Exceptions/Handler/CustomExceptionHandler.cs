using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlock.Exceptions.Handler;

/// <summary>
/// Custom exception handler to handle exceptions and return a consistent response.
/// </summary>
/// <param name="logger"></param>
public class CustomExceptionHandler
    // Uses logger to log the exception
    (ILogger<CustomExceptionHandler> logger)
    // Implements the IExceptionHandler interface , a global handling interface 
    : IExceptionHandler
{
     /// <summary>
     /// Handles the exception and returns a consistent response.
     /// </summary>
     /// Intercept before they reach the client
     /// log error details 
     /// converts exceptions into structures HTTP  response
     /// <param name="context"></param>
     /// <param name="exception"></param>
     /// <param name="cancellationToken"></param>
     /// <returns></returns>
     public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
     {
          // Log the error message and time of occurrence in UTC 
          logger.LogError(
              "Error Message: {exceptionMessage}, Time of occurrence {time}",
              exception.Message, DateTime.UtcNow);

          // Switch case to handle different types of exceptions
          //Maps different exception types to specific HTTP status codes
          //Use pattern matching to get the exception details
          // _ default case to handle all other exceptions
          (string Detail, string Title, int StatusCode) details = exception switch
          {

               InternalServerException =>
               (
                   exception.Message,
                   exception.GetType().Name,
                   context.Response.StatusCode = StatusCodes.Status500InternalServerError
               ),
               ValidationException =>
               (
                   exception.Message,
                   exception.GetType().Name,
                   context.Response.StatusCode = StatusCodes.Status400BadRequest
               ),
               BadRequestException =>
               (
                   exception.Message,
                   exception.GetType().Name,
                   context.Response.StatusCode = StatusCodes.Status400BadRequest
               ),
               ConflictException =>
               (
               exception.Message,
               exception.GetType().Name,
               context.Response.StatusCode = StatusCodes.Status409Conflict
               ),
               NotFoundException =>
               (
                   exception.Message,
                   exception.GetType().Name,
                   context.Response.StatusCode = StatusCodes.Status404NotFound
               ),
               _ =>
               (
                   exception.Message,
                   exception.GetType().Name,
                   context.Response.StatusCode = StatusCodes.Status500InternalServerError
               )
          };
          // Create a problem details object to return as a response
          var problemDetails = new ProblemDetails
          {
               // Exception type
               Title = details.Title,
               // Error message
               Detail = details.Detail,
               // Status code
               Status = details.StatusCode,
               // The request path where the error occurred.
               Instance = context.Request.Path
          };
          // Add the trace identifier to the response
          problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
          // Add the validation errors to the response
          if (exception is ValidationException validationException)
          {
               problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
          }
          // Return the problem details as a JSON response
          await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
          return true;
          //Register inprogram.cs 
          //builder.Services.AddExceptionHandler<CustomExceptionHandler>();
          //app.UseExceptionHandler();

     }
}
//PAttern matching
//Pattern matching is a feature that allows you to match a value against a pattern.
//It is a powerful feature that allows you to write more readable and maintainable code.
//It replace  the switch statement with a more concise and readable syntax.

//Switch case 
//void CheckType(object obj)
//    {
//        switch(obj)
//        {
//            case int:
//                Console.WriteLine("It's an integer");
//                break;
//            case string:
//                Console.WriteLine("It's a string");
//                break;
//            default:
//                Console.WriteLine("It's something else");
//                break;
//        }
//    }

//Using Pattern matching  in else if 
//void CheckType(object obj)
//    {
//        if (obj is int)
//        {
//            Console.WriteLine("It's an integer");
//        }
//        else if (obj is string)
//        {
//            Console.WriteLine("It's a string");
//        }
//        else
//        {
//            Console.WriteLine("It's something else");
//        } 
//    }

//Pattern matching in switch 
//string GetStatus(int StatusCode) =>
//           StatusCode switch
//           {
//               200 => "OK",
//               404 => "Not Found",
//               500 => "Internal Server Error",
//               _ => "Unknown"
//           };
//    Console.WriteLine(GetStatus(200)); 