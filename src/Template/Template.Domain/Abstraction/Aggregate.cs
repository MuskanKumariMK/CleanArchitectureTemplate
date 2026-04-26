namespace Template.Domain.Abstraction
{
     /// <summary>
     /// Aggregate interface
     /// Aggregate root class that extends Entity<TId> and implements IAggregate<TId>.
     /// Manages domain events, which are used to track state changes in the entity.
     /// </summary>
     /// <typeparam name="TId"></typeparam>
     public class Aggregate<TId> : Entity<TId>, IAggregate<TId>
     {
          /// <summary>
          /// List of domain events that have occurred on the entity.
          /// </summary>
          private readonly List<IDomainEvent> _domainEvents = new();
          /// <summary>
          /// Gets the list of domain events that have occurred on the entity.
          /// Provides a read-only list of domain events for external access.
          /// </summary>
          public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
          /// <summary>
          /// Adds a domain event to the list of domain events that have occurred on the entity.
          /// </summary>
          /// <param name="domainEvent"></param>
          public void AddDomainEvent(IDomainEvent domainEvent)
          {
               //Add domain event to the list of domain events
               _domainEvents.Add(domainEvent);
          }
          /// <summary>
          /// Clears the list of domain events that have occurred on the entity.
          /// </summary>
          /// <returns></returns>
          public IDomainEvent[] ClearDomainEvents()
          {
               IDomainEvent[] domainEvents = _domainEvents.ToArray();
               _domainEvents.Clear();
               return domainEvents;
          }
     }
}
