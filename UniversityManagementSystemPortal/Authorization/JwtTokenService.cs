using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.ModelDto;

namespace UniversityManagementSystemPortal.Authorization
{

  public interface IJwtTokenService
    {
        string GenerateJwtToken(User user);
        Guid? ValidateJwtToken(string token);
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
            // get user roles
            var userRoles = _context.UserRoles.Include(ur => ur.Role).Where(ur => ur.UserId == user.Id).ToList();

            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
            new Claim("id", user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            // add user roles as claims
            new Claim(ClaimTypes.Role, string.Join(",", userRoles.Select(ur => ur.Role.Name)))
        }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = "https://localhost:7092",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public Guid? ValidateJwtToken(string token)
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
                var idClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "id");
                if (idClaim == null || !Guid.TryParse(idClaim.Value, out Guid userId))
                {
                    return null;
                }

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
