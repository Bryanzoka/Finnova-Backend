using System.Security.Claims;
using Finnova.Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace Finnova.Infrastructure.Services
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return int.TryParse(value, out int id) ? id : throw new UnauthorizedAccessException("user not authenticated");
            }
        }

        public bool IsInRole(string role) => _httpContextAccessor.HttpContext?.User?.IsInRole(role) ?? false;
    }
}