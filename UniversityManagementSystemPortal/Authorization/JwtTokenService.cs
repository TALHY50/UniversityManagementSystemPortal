using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.ModelDto;

namespace UniversityManagementSystemPortal.Authorization
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
        bool ValidateToken(string token);
        string RefreshToken(string token);
    }

    public class JwtTokenService : IJwtTokenService
    {
        private readonly UmspContext _context;
        private readonly AppSettings _appSettings;

        public JwtTokenService(IOptions<AppSettings> appSettings, UmspContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var roles = user.UserRoles.Select(ur => ur.Role.Name).ToArray();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("id", user.Id.ToString()),
                new Claim("username", user.Username),
                new Claim(ClaimTypes.Role, string.Join(",", roles))
            }),
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.TokenExpirationMinutes),
                Issuer = "https://localhost:7003",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ValidateToken(string token)
        {
            if (token == null)
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return validatedToken is JwtSecurityToken;
            }
            catch
            {
                return false;
            }
        }

        public string RefreshToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            // Validate token
            if (!tokenHandler.CanReadToken(token))
                throw new SecurityTokenException("Invalid token");

            var jwtToken = tokenHandler.ReadJwtToken(token);

            // Get claims from the token
            var claims = jwtToken.Claims;

            // Check if the token has the necessary claims to refresh
            if (!claims.Any(c => c.Type == "id" && Guid.TryParse(c.Value, out _)) ||
                !claims.Any(c => c.Type == "username"))
                throw new SecurityTokenException("Invalid token");

            var userId = Guid.Parse(claims.First(c => c.Type == "id").Value);
            var username = claims.First(c => c.Type == "username").Value;

            // Get user by ID and username
            var user = _context.Users.FirstOrDefault(u => u.Id == userId && u.Username == username);

            if (user == null)
                throw new SecurityTokenException("User not found");

            // Generate new token
            return GenerateToken(user);
        }
    }


}
