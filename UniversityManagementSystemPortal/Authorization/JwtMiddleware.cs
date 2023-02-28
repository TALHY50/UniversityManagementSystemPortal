namespace UniversityManagementSystemPortal.Authorization
{ 

    public class JwtMiddleware : IMiddleware
{
    private readonly IJwtTokenService _jwtAuth;

    public JwtMiddleware(IJwtTokenService jwtAuth)
    {
        _jwtAuth = jwtAuth;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // Check if the HTTP request has an Authorization header with a JWT token
        if (context.Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            // Extract the JWT token from the Authorization header
            var token = authHeader.ToString().Split(" ").Last();

            // Validate the JWT token
            var userId = _jwtAuth.ValidateJwtToken(token);

            if (userId != null)
            {
                // If the JWT token is valid, set the authenticated user ID in the HTTP context
                context.Items["UserId"] = userId;
            }
        }

        // Call the next middleware in the pipeline
        await next(context);
    }
}


}
