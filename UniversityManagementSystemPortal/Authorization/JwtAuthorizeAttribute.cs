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
    using UniversityManagementSystemPortal.Enum;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JwtAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly RoleType[] _roles;

        public JwtAuthorizeAttribute(params RoleType[] roles)
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
            var jwtTokenService = context.HttpContext.RequestServices.GetService<IJwtTokenService>();
            var userId = jwtTokenService?.ValidateJwtToken(token);

            if (userId == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Check if user has the required roles
            if (_roles != null && _roles.Length > 0)
            {
                var requiredRoles = _roles.Select(r => Enum.GetName(typeof(RoleType), r)).ToArray();
                var hasRequiredRole = context.HttpContext.User.Claims.Any(c => c.Type == ClaimTypes.Role && requiredRoles.Contains(c.Value));
                if (!hasRequiredRole)
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }
        }
    }



}
