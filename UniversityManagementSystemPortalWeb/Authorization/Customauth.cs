using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using UniversityManagementSystemPortal;
using UniversityManagementSystemPortal.Authorization;

namespace UniversityManagementSystemPortalWeb.Authorization
{
    public class CustomAuth : Attribute, IAuthorizationFilter
    {
        private readonly IList<string> _roles;
    
        public CustomAuth(params string[] roles)
        {
            _roles = roles ?? new string[] { };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
            {
                return;
            }

            // authorization
            var userIdClaim = context.HttpContext.User.FindFirst("UserId");
            var rolesClaims = context.HttpContext.User.FindFirstValue(ClaimTypes.Role);

            // Split the rolesClaims into a list of roles
            var userRoles = rolesClaims?.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries) ?? new string[] { };

            if (userIdClaim == null || (_roles.Any() && _roles.All(role => userRoles.Contains(role))))
            {
                // not logged in or role not authorized
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }   
}
