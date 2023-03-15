using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Enum;
using System.Linq;

namespace UniversityManagementSystemPortal.Authorization
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    namespace UniversityManagementSystemPortal.Authorization
    {
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        public class JwtAuthorizeAttribute : Attribute, IAuthorizationFilter
        {
            private readonly IList<string> _roles;

            public JwtAuthorizeAttribute(params string[] roles)
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
                var userId = context.HttpContext.Items["User"] as User;
                if (userId == null || (_roles.Any() && !_roles.Any(role => userId.UserRoles.Any(userRole => userRole.Role.Name == role))))
                {
                    // not logged in or role not authorized
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
}   }   }







