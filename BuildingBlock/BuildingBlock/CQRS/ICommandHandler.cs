using MediatR;

namespace BuildingBlock.CQRS
{ 
    /// <summary>
  /// Defines a handler for commands that do not return a result.
  /// </summary>
  /// <typeparam name="TCommand">
  /// The type of the command. This must implement ICommand<Unit>, ensuring that the command
  /// follows the pattern of not returning any meaningful result (Unit being MediatR's equivalent to void).
  /// </typeparam>
  /// <remarks>
  /// This interface is useful when the operation modifies the state but does not need to return data,
  /// such as a delete operation or a simple update that doesn't require confirmation.
  /// </remarks>
    //It does not return a result (i.e., it returns Unit, which is MediatR’s equivalent of void).
    public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
        where TCommand : ICommand<Unit>
    {
    }
    /// <summary>
    /// Defines a handler for commands that return a result.
    /// </summary>
    /// <typeparam name="TCommand">
    /// The type of the command. This must implement ICommand<TResponse>, ensuring a consistent command structure.
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// The type of the result returned by the command. The 'notnull' constraint ensures that a non-null result is provided.
    /// </typeparam>
    /// <remarks>
    /// This interface extends MediatR's IRequestHandler, allowing the command to be processed through MediatR's pipeline.
    /// Use this for operations that modify state and need to return data, such as creating a new entity and returning its identifier.
    /// </remarks>
    //It returns a result. 
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
        where TResponse : notnull
    {
    }
}
