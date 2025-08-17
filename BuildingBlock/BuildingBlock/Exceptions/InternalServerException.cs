namespace BuildingBlock.Exceptions
{
     /// <summary>
     /// Represents an exception that occurs when an internal server error is encountered.
     /// </summary>
     public class InternalServerException : Exception
     {
          /// <summary>
          /// Constructor for InternalServerException with an error message
          /// </summary>
          /// <param name="messaage"></param>
          public InternalServerException(string messaage) : base(messaage)
          {

          }
          /// <summary>
          /// Constructor for InternalServerException with an error message and details
          /// </summary>
          /// <param name="messaage"></param>
          /// <param name="details"></param>
          public InternalServerException(string messaage, string details) : base(messaage)
          {
               Details = details;
          }
          /// <summary>
          /// Stores extra error details for debugging.
          /// </summary>
          public string? Details { get; }
     }
}
