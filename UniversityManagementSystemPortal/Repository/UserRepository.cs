using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.Interfce;
using UniversityManagementSystemPortal.ModelDto;
using UniversityManagementSystemPortal.ModelDto.NewFolder;
using UniversityManagementSystemPortal.ModelDto.UserDto;

namespace UniversityManagementSystemPortal.Repository
{

    public class UserRepository : IUserInterface
    {
        private readonly UmspContext _context;
        private IJwtTokenService _jwtTokenService;
        private readonly AppSettings _appSettings;
        public UserRepository(UmspContext context,
            IJwtTokenService jwtTokenService,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
            _appSettings = appSettings.Value;
        }
        public LoginView Authenticate(Login model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // validate
            if (user == null)
                throw new AppException("Username or password is incorrect");

            // authentication successful
            var jwtToken = _jwtTokenService.GenerateJwtToken(user);

            return new LoginView(user, jwtToken);
        }

        public async Task<User> RegisterAsUser(User user)
        {
            user.EmailConfirmed = false;
            user.IsActive = true;
            user.CreatedAt = DateTime.UtcNow;
            user.Id = Guid.NewGuid();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }




        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.RemoveRange(user);
            await _context.SaveChangesAsync();
        }
    }
}

