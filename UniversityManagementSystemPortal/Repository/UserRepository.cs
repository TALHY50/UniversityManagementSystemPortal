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

    public class UserRepository : IUserRepository
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

        public async Task<User> RegisterAsUser(User model)
        {
            try
            {
                if (_context.Users.Any(x => x.Username == model.Username))
                    throw new AppException("Username '" + model.Username + "' is already taken");

                // set other properties
                model.Id = Guid.NewGuid(); // generate a unique ID
                model.EmailConfirmed = false;
                model.IsActive = false;

                // create a new UserRole object and assign it to the user
                var defaultRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleType == RoleType.SuperAdmin);
                var userRole = new UserRole
                {
                    User = model,
                    Role = defaultRole
                };
                model.UserRoles.Add(userRole);

                _context.Users.Add(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw new AppException($"An error occurred while registering user: {ex.Message}");
            }
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
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}

