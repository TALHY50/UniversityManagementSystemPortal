using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Models.DbContext;

namespace UniversityManagementSystemPortal.Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly UmspContext _context;

        public UserRoleRepository(UmspContext context)
        {
            _context = context;
        }

        public async Task<UserRole> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            var userRole = await _context.UserRoles
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync(ur => ur.Id == id);

            return userRole;
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            var userRoles = await _context.UserRoles
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .ToListAsync();

            return userRoles;
        }

        public async Task AddAsync(UserRole userRole)
        {
            if (userRole == null)
            {
                throw new ArgumentNullException(nameof(userRole));
            }
            userRole.Id = Guid.NewGuid();
            await _context.UserRoles.AddAsync(userRole);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(UserRole userRole)
        {
            if (userRole == null)
            {
                throw new ArgumentNullException(nameof(userRole));
            }

            _context.UserRoles.Update(userRole);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(UserRole userRole)
        {
            if (userRole == null)
            {
                throw new ArgumentNullException(nameof(userRole));
            }

            _context.UserRoles.Remove(userRole);
            await SaveChangesAsync();
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }




}
