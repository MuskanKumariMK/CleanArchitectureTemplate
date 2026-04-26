namespace Security.Applications.Common.Exceptions
{
     /// <summary>
     /// Invalid Credential Custom Exception
     /// </summary>
     public class InvalidCredentialsException : Exception
     {
          public InvalidCredentialsException(string message)
              : base(message)
          {
          }
     }
}
