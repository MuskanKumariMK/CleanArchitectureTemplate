using BuildingBlock.CQRS;
using FluentValidation;

namespace BuildingBlock.Behaviour
{
     /// <summary>
     /// Creatind mediatr pipeline behaviour for validation ,which executes before and after a the request is handled.
     /// </summary>
     /// <typeparam name="TRequest"></typeparam>
     /// <typeparam name="Tresponse"></typeparam>
     /// <param name="validators"></param>
     public class ValidationBehaviour<TRequest, Tresponse>
         //A collection of validators to validate for TRequest
         (IEnumerable<IValidator<TRequest>> validators)

         : IPipelineBehavior<TRequest, Tresponse>
         // It ensure only ICommand<Tresponse> can be handled by this behaviour not query go tthrough this validation pipeline
         where TRequest : ICommand<Tresponse>

     {
          /// <summary>
          /// This method calls before and after MediatR handles a request
          /// Ensure all defined FluentValidation rules are applied.
          /// Call the next handler only if validation passes.
          /// </summary>
          /// <param name="request"></param> : Incoming request to be validated
          /// <param name="next"></param> : The next handler in the pipeline
          /// <param name="cancellationToken"></param> : The cancellation token
          /// <returns></returns>
          /// <exception cref="ValidationException"></exception>
          public async Task<Tresponse> Handle(TRequest request, RequestHandlerDelegate<Tresponse> next, CancellationToken cancellationToken)
          {
               //Create a validation context for the request
               //This context is passed to all registered validators
               var context = new ValidationContext<TRequest>(request);
               //Execute all validators asynchronously
               //Task.WhenAll() is used to execute all validators concurrently (parallel)
               var validationResult = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
               //Extracts validation error from all validators 
               // If a validation error is found, it return a list of validation failures
               var failures = validationResult
                   .SelectMany(r => r.Errors)
                   .Where(f => f != null)
                   .ToList();
               //If there are validation failures, throw a ValidationException
               if (failures.Any())
               {
                    throw new ValidationException(failures);
               }
               //If there are no validation failures, call the next handler in the pipeline
               return await next();
          }
     }
}
