using System.Security.Claims;
using UniversityManagementsystem.Models;

namespace UniversityManagementSystemPortal.IdentityServices
{
    public interface IIdentityServices
    {
        public Guid? GetUserId();
    }
    public class IdentityService : IIdentityServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public Guid? GetUserId()
        {
            var claimsPrincipal = _httpContextAccessor.HttpContext?.User;
            var userId = claimsPrincipal?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null || !Guid.TryParse(userId, out var guidUserId))
            {
                return null;
        }

            return guidUserId;
        }
    }
}
