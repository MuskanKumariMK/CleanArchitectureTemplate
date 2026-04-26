namespace Template.Application.Interface
{
     public interface IUserContext
     {
          string? UserId { get; }
          string? UserName { get; }
          string? Email { get; }
     }
}
