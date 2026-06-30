using System.Security.Claims;
using ToDoList.Gateway.Application.Interfaces;

namespace ToDoList.Gateway.WebAPI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<Claim> Claims =>
            _httpContextAccessor.HttpContext?.User?.Claims
            ?? Enumerable.Empty<Claim>();
    }
}
