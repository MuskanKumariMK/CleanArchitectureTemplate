namespace Template.Domain.Abstraction
{
     /// <summary>
     /// Aggregate interface with generic type
     /// Generic interface for an aggregate root.
     /// Extends IEntity<T> and IAggregate, meaning it includes both entity and domain event functionality.
     /// </summary>
     /// <typeparam name="T"></typeparam>
     public interface IAggregate<T> : IAggregate, IEntity<T>
     { }
     /// <summary>
     /// Aggregate interface
     /// Base interface for aggregates.
     /// Extends IEntity and adds domain event management functionality.
     /// </summary>
     public interface IAggregate : IEntity
     {
          /// <summary>
          /// Gets the list of domain events that have occurred on the entity.
          /// </summary>
          IReadOnlyList<IDomainEvent> DomainEvents { get; }
          /// <summary>        
          /// Clears and returns all domain events.  
          /// </summary>
          IDomainEvent[] ClearDomainEvents();
     }
}
