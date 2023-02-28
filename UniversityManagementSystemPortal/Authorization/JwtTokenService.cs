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
        string GenerateJwtToken(User user);
        int? ValidateJwtToken(string token);
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

        public string GenerateJwtToken(User user)
        {
            // Get the user's roles
            var roles = _context.UserRoles
        .Where(ur => ur.UserId == user.Id)
        .Select(ur => ur.Role);

            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
            new Claim("id", user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            // Add the user's roles to the claims
            new Claim(ClaimTypes.Role, string.Join(",", roles.Select(r => r.Name)))
        }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = "https://localhost:7003",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public int? ValidateJwtToken(string token)
        {
            if (token == null)
                return null;

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
                    ValidIssuer = "https://localhost:7003",
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }

}
