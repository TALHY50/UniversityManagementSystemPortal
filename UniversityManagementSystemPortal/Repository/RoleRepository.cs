using Microsoft.EntityFrameworkCore;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Repository
{
    public class RoleRepository : IRoleInterface
    {
        private readonly UmspContext _context;

        public RoleRepository(UmspContext context)
        {
            _context = context;
        }

        public async Task<Role> GetByIdAsync(Guid id)
        {
            var role = await _context.Roles
                .Include(r => r.UserRoles)
                .ThenInclude(ur => ur.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            return role;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> CreateAsync(Role role)
        {
            role.Id = Guid.NewGuid();
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Role role)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }

        public async Task<Role> GetByRoleTypeAsync(int roleType)
        {
            var role = await _context.Roles
                .Include(r => r.UserRoles)
                .ThenInclude(ur => ur.User)
                .FirstOrDefaultAsync(r => r.RoleType == roleType);

            return role;
        }
    }
}
