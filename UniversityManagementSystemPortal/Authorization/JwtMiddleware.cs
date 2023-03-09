using Microsoft.Extensions.Options;
using UniversityManagementSystemPortal.Interfce;
using UniversityManagementSystemPortal.ModelDto;
using UniversityManagementSystemPortal.Repository;

namespace UniversityManagementSystemPortal.Authorization
{ 

    public class JwtMiddleware
    { 

        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private readonly string[] _roles;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings, string[] roles = null)
        {
            _next = next;
            _appSettings = appSettings.Value;
            _roles = roles;
        }


        public async Task InvokeAsync(HttpContext context, IUserInterface _userRepository, IJwtTokenService jwtTokenService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var userId = jwtTokenService.ValidateJwtToken(token);
                if (userId != null)
                {
                    var user =await _userRepository.GetByIdAsync(userId.Value);
                    context.Items["User"] = user;
                }
            }

            await _next(context);
        }

    }
}