namespace BuildingBlock.Behaviour.Interface
{
     /// <summary>
     /// Interface for authorization services that handle authorization logic for specific request types.
     /// </summary>
     /// <typeparam name="TRequest"></typeparam>
     public interface IAuthorizationService<TRequest>
     {
          /// <summary>
          /// Authorizes the given request asynchronously.
          /// </summary>
          /// <param name="request"></param>
          /// <param name="cancellationToken"></param>
          /// <returns></returns>
          Task Authorize(TRequest request, CancellationToken cancellationToken);
     }
}
