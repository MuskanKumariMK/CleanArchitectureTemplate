namespace BuildingBlock.Exceptions
{
   public  class BadRequestException : Exception
    {
        /// <summary>
        /// Constructor for BadRequestException with an error message
        /// Allow throw exception with a message throw new BadException("Invalid request data.")
        /// </summary>
        /// <param name="messaage"></param>
        public BadRequestException(string messaage) : base(messaage)
        {

        }
        /// <summary>
        /// Constructor for BadRequestException with an error message and details
        ///Allow throw exception with a message and details throw new BadException("Invalid request data.", "The request data is invalid.")
        /// </summary>
        /// <param name="messaage"></param>
        /// <param name="details"></param>
        public BadRequestException(string messaage, string details) : base(messaage)
        {
            Details = details;
        }
        /// <summary>
        /// 
        /// </summary>
        public string? Details { get; }

    }
}
