using MediatR;

namespace BuildingBlock.CQRS
{
    /// <summary>
    /// Defines a handler for a query that returns a result.
    /// </summary>
    /// <typeparam name="TQuery">
    /// The type of the query. This must implement the IQuery<TResponse> interface,
    /// ensuring that it conforms to the expected query structure.
    /// </typeparam>
    /// <typeparam name="TResponse">
    /// The type of the response that the query returns.
    /// The 'notnull' constraint ensures that a valid, non-null response is always provided.
    /// </typeparam>
    /// <remarks>
    /// This interface extends MediatR's IRequestHandler, enabling the use of MediatR's pipeline
    /// and handling mechanisms for query operations. In a CQRS pattern, queries are used exclusively
    /// for retrieving data without modifying any state.
    /// </remarks> //It returns a result. 
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
        where TResponse : notnull
    {
    }
}
