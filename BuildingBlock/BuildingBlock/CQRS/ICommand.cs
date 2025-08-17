using MediatR;
using static System.Net.Mime.MediaTypeNames;

namespace BuildingBlock.CQRS
{
    /// <summary>
    /// Represents a command request in a CQRS pattern that does not return a meaningful result.
    /// This interface is used for operations that change the state of the system but do not need to return data.
    /// It utilizes MediatR's <see cref="Unit"/> as a placeholder for a void return type.
    /// </summary>
    //ICommand represents a command that does not return a result (i.e., it returns Unit, which is MediatR’s equivalent of void).
    //It inherits from ICommand<Unit>, ensuring consistency.
    public interface ICommand : ICommand<Unit>
    {
    }
    /// <summary>
    /// Represents a command request in a CQRS pattern that expects a response of type TResponse.
    /// This interface is used for operations that change the system's state and need to return data,
    /// such as creating a new record and returning its identifier.
    /// </summary>
    /// <typeparam name="TResponse">
    /// The type of the response returned by the command.
    /// The 'notnull' constraint ensures that the response is always a valid non-null value.
    /// </typeparam>
    //Commands modify the state of the application and optionally return a result.
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {

    }
   
}
