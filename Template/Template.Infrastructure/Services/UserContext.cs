using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Template.Application.Interface;

namespace Template.Infrastructure.Services
{
     public class UserContext(IHttpContextAccessor _httpContextAccessor) : IUserContext
     {

          public string? UserId =>
              _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

          public string? UserName =>
              _httpContextAccessor.HttpContext?.User?.Identity?.Name;

          public string? Email =>
              _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
     }
}
