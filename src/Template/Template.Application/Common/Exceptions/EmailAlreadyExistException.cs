namespace Security.Applications.Common.Exceptions
{
     /// <summary>
     /// Custom Exception for the email already exists 
     /// </summary>
     public class EmailAlreadyExistException : ApplicationException
     {
          public EmailAlreadyExistException(string email)
               : base($"Email '{email}' is already registered.")
          {
          }
     }
}
