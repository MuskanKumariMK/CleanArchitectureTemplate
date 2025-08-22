namespace Template.Domain.Abstraction
{
     /// <summary>
     /// Domain event interface
     /// INotifcation is a MediatR interface
     /// It is allowing domain events to be dispatched through mediator handlers
     /// Represents a domain event that is dispatched through a mediator.
     /// Implements MediatR's INotification interface, allowing event-driven behavior in the domain.
     /// </summary>
     public interface IDomainEvent : INotification
     {
          /// <summary>
          /// Unique identifier for the domain event.
          /// Uses a new GUID for each event instance.
          /// </summary>
          Guid EventId => Guid.NewGuid();
          /// <summary>
          /// Timestamp when the event occurred.
          /// Uses DateTime.Now to capture the current time.
          /// </summary>
          public DateTime OccurredOn => DateTime.Now;
          /// <summary>
          /// Identifies the event type using the assembly-qualified name of the class.
          /// </summary>
          public string EventType => GetType().AssemblyQualifiedName;
     }
}
/// <summary>
///IEntity & IEntity<T> – Define common properties for all entities, including auditing fields.
///Entity<T> – An abstract class implementing IEntity<T>, providing a base for concrete entity classes.
///Aggregate<TId> – Extends Entity<TId> and introduces domain event handling.
///IAggregate & IAggregate<T> – Define contract for aggregates, including domain event management.
///IDomainEvent – Represents a domain event that follows the MediatR pattern for event handling.
/// </summary>

