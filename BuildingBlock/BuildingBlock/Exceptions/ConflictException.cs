namespace BuildingBlock.Exceptions
{
     /// <summary>
     /// Represents an exception that occurs when a conflict is detected, such as a duplicate entry.
     /// </summary>
     public class ConflictException : Exception
     {
          /// <summary>
          /// Constructor for ConflictException with an error message.
          /// </summary>
          public ConflictException(string message) : base(message)
          {
          }

          /// <summary>
          /// Constructor for ConflictException with additional details.
          /// </summary>
          /// <param name="name">The name of the entity or resource.</param>
          /// <param name="reason">The reason for the conflict.</param>
          public ConflictException(string name, string reason)
              : base($"Conflict with \"{name}\". Reason: {reason}.")
          {
               Name = name;
               Reason = reason;
          }

          /// <summary>
          /// The name of the resource/entity that caused the conflict.
          /// </summary>
          public string? Name { get; }

          /// <summary>
          /// The detailed reason for the conflict.
          /// </summary>
          public string? Reason { get; }
     }
}
