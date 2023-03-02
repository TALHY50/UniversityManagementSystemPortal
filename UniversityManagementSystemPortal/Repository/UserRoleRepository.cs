using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Repository
{
    public class UserRoleRepository : IUserRoleInterface
    {
        private readonly UmspContext _context;

        public UserRoleRepository(UmspContext context)
        {
            _context = context;
        }

        public async Task AddUserRoleAsync(Guid roleId, Guid userId)
        {
            var userRole = new UserRole {Id= Guid.NewGuid(), RoleId = roleId, UserId = userId };
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserRoleAsync(Guid roleId, Guid userId)
        {
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.RoleId == roleId && ur.UserId == userId);
            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<UserRole> GetByIdAsync(Guid id)
        {
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.Id == id);
            return userRole;
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            var userRoles = await _context.UserRoles.ToListAsync();
            return userRoles;
        }

        public async Task UpdateAsync(UserRole userRole)
        {
            _context.UserRoles.Update(userRole);
            await _context.SaveChangesAsync();
        }
    }


}
