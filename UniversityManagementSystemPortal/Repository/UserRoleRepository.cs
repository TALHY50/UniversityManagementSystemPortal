using Microsoft.EntityFrameworkCore;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Interfaces;

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
            return await _context.UserRoles
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync(ur => ur.Id == id);
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            return await _context.UserRoles.ToListAsync();
        }

        public async Task<UserRole> CreateAsync(UserRole userRole)
        {
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
            return userRole;
        }

        public async Task UpdateAsync(UserRole userRole)
        {
            _context.UserRoles.Update(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserRole userRole)
        {
            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();
        }
    }
}
