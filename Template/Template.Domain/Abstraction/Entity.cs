namespace Template.Domain.Abstraction
{
     public abstract class Entity<T> : IEntity<T>
     {
          /// <summary>
          /// Gets or sets the unique identifier of the entity.
          /// </summary>
          public T Id { get; set; }
          /// <summary>
          /// Timestamp when the entity was created.
          /// </summary>
          public DateTime? CreatedAt { get; set; }
          /// <summary>
          /// Identifier of the user who created the entity.
          /// </summary>
          public string? CreatedBy { get; set; }
          /// <summary>
          /// Timestamp when the entity was last updated.
          /// </summary>
          public DateTime? UpdatedAt { get; set; }
          /// <summary>
          /// Identifier of the user who last updated the entity.
          /// </summary>
          public string? UpdatedBy { get; set; }
     }
}
