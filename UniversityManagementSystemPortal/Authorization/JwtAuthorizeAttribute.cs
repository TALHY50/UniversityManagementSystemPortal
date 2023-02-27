using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace UniversityManagementSystemPortal.Authorization
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Linq;
    using System.Security.Claims;
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JwtAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;

        public JwtAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Allow anonymous access
            if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
                return;

            // Get authorization header
            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            // Check if header exists and starts with "Bearer"
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer"))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Get token from header
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();

            // Check if token is valid
            var isTokenValid = context.HttpContext.RequestServices.GetService(typeof(IJwtTokenService)) is IJwtTokenService jwtTokenService &&
                               jwtTokenService.ValidateToken(token);

            if (!isTokenValid)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Check if user has the required roles
            if (_roles != null && _roles.Length > 0)
            {
                var hasRequiredRole = context.HttpContext.User.Claims.Any(c => c.Type == ClaimTypes.Role && _roles.Contains(c.Value));
                if (!hasRequiredRole)
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }
        }
    }


}
