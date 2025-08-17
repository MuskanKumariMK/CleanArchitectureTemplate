namespace BuildingBlock.Behaviour.Interface
{
     /// <summary>
     /// ILoggerService interface for logging requests and results in a generic way.
     /// </summary>
     /// <typeparam name="TRequest"></typeparam>
     public interface ILoggerService<TRequest>
     {
          /// <summary>
          /// Logs the request and result of a specific request type asynchronously.
          /// </summary>
          /// <param name="request"></param>
          /// <param name="result"></param>
          /// <returns></returns>
          Task Log(TRequest request, string result);
     }
}
