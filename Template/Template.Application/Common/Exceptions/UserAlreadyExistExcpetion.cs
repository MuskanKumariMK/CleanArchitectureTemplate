namespace Security.Applications.Common.Exceptions
{
     /// <summary>
     /// Custom Exception for User already exists
     /// </summary>
     public class UserAlreadyExistExcpetion : ApplicationException
     {
          public UserAlreadyExistExcpetion(string username)
               : base($"User with username '{username}' already exists.")
          {
          }
     }
}
