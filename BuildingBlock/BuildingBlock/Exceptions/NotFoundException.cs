namespace BuildingBlock.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when an entity is not found.
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Constructor for NotFoundException with an error message
        /// Allow throw exception with a message throw new NotFoundException("Entity not found.")
        /// </summary>
        /// <param name="message"></param>
        public NotFoundException(string message) : base(message)
        {
            
        }
        /// <summary>
        /// Constructor for NotFoundException with an error message and details
        /// Allow throw exception with a message and details throw new NotFoundException("Entity not found.", "The entity was not found.")
        /// </summary>
        /// <param name="name"></param>
        /// <param name="key"></param>
        public NotFoundException(string name ,object key) : base($"Entity \"{name}\" ({key}) was not found.")
        {

        }
    }
}
