using BuildingBlock.Exceptions;

namespace Security.Applications.Common.Exceptions
{
     /// <summary>
     /// Custom Exception for User not Found
     /// </summary>
     public class UserNotFoundException : NotFoundException
     {
          public UserNotFoundException(string username) : base("user", username)
          {
          }
     }
}
