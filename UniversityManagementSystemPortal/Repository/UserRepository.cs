using Microsoft.EntityFrameworkCore;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfce;
using UniversityManagementSystemPortal.ModelDto.NewFolder;
using BCryptNet = BCrypt.Net.BCrypt;

namespace UniversityManagementSystemPortal.Repository
{

    public class UserRepository : IUserInterface
    {
        private readonly UmspContext _context;

        public UserRepository(UmspContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterAsUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User object is null.");
            }

            user.EmailConfirmed = false;
            user.IsActive = true;
            //user.CreatedAt = DateTime.UtcNow;
            user.Id = Guid.NewGuid();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new AppException($"User with ID '{id}' does not exist.");
            }

            return user;
        }


        public async Task UpdateAsync(User user)
        {
            if (user == null)
            {
                throw new AppException(nameof(user), "User object is null.");
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            if (user == null)
            {
                throw new AppException(nameof(user), "User object is null.");
            }

            _context.Users.RemoveRange(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> Authenticate(Login model)
        {
            if (model == null)
            {
                throw new AppException(nameof(model), "Login object is null.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);

            if (user == null)
            {
                return null;
            }
            return user;
        }
    }


}

