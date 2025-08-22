namespace Template.Domain.Abstraction
{
     /// <summary>
     /// Entity interface with generic type
     /// Generic interface for an entity that has an ID of type T.
     /// Extends IEntity, meaning it also has audit properties like CreatedAt, CreatedBy, etc.
     /// </summary>
     /// <typeparam name="T"></typeparam>
     public interface IEntity<T> : IEntity
     {
          /// <summary>
          /// Gets or sets the unique identifier of the entity.
          /// Id is of type T
          /// </summary>
          public T Id { get; set; }
     }
     /// <summary>
     /// Entity interface 
     /// Base interface for all entities.
     /// Contains common properties for tracking creation and modification details.
     /// </summary>
     public interface IEntity
     {
          /// <summary>
          /// Entity has CreatedAt
          /// Timestamp when the entity was created.
          /// Nullable to indicate it may not always be set.
          /// </summary>
          public DateTime? CreatedAt { get; set; }
          /// <summary>
          /// Entity has CreatedBy
          /// Identifier of the user who created the entity.
          /// Nullable for cases where creator details are not available.
          /// </summary>
          public string? CreatedBy { get; set; }
          /// <summary>
          /// Entity has UpdatedAt
          /// Timestamp when the entity was last updated.
          /// Nullable to indicate it may not always be modified.
          /// </summary>
          public DateTime? UpdatedAt { get; set; }
          /// <summary>
          /// Entity has UpdatedBy
          /// Identifier of the user who last updated the entity.
          /// Nullable for cases where updater details are not available.
          /// </summary>
          public string? UpdatedBy { get; set; }
     }
}
