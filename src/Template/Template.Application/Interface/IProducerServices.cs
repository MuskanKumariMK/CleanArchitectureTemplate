namespace Template.Application.Interface
{
     public interface IProducerServices
     {
          /// <summary>
          /// Produces a message to the specified topic asynchronously.
          /// </summary>
          /// <param name="topic"></param>
          /// <param name="message"></param>
          /// <param name="cancellationToken"></param>
          /// <returns></returns>
          Task ProduceAsync(string topic, string message, CancellationToken cancellationToken = default);
     }
}
