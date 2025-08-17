using MediatR;

namespace BuildingBlock.CQRS
{
    /// <summary>
    /// Represents a query request in a CQRS pattern that expects a response of type TResponse.
    /// This interface is intended for operations that fetch data without modifying the system's state.
    /// </summary>
    /// <typeparam name="TResponse">
    /// The type of response returned by the query.
    /// The 'notnull' constraint ensures that the response is never null.
    /// </typeparam>
    //Queries are used to retrieve data without modifying the state.
    public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
    {

    }
}
