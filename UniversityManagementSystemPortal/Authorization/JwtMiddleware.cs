namespace UniversityManagementSystemPortal.Authorization
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using UniversityManagementSystemPortal.Interfce;

    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly IUserRepository _userRepository;
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IJwtTokenService jwtTokenService)
        {
            string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null && jwtTokenService.ValidateToken(token))
            {
                // User is authenticated
                await _next(context);
            }
            else if (context.Request.Path.StartsWithSegments("/api/authenticate"))
            {
                // Allow anonymous access to the authentication endpoint
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 401;
            }
        }
    }

}
