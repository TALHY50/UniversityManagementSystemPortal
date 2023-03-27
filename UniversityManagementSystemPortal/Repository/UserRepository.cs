using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.NewFolder;
using UniversityManagementSystemPortal.Models.DbContext;

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
                return null;
            }

            user.EmailConfirmed = false;
            user.IsActive = true;
            user.Id = Guid.NewGuid();
            await _context.Users.AddAsync(user);
            await SaveChangesAsync();

            return user;
        }

        public  Task<IQueryable<User>> GetAllAsync()
        {
            var user =  _context.Users.AsQueryable()
                .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
                .AsNoTracking();
            return Task.FromResult(user);
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
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            if (user == null)
            {
                throw new AppException(nameof(user), "User object is null.");
            }

            _context.Users.RemoveRange(user);
            await SaveChangesAsync();
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
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Set<User>().SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<int> GetUniqueUsernameNumberAsync(string username)
        {
            int uniqueNumber = 1;

            while (await _context.Users.AnyAsync(u => u.Username == username + uniqueNumber))
            {
                uniqueNumber++;
            }

            return uniqueNumber;
        }
        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }


}

