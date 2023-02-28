using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniversityManagementsystem.Models;

namespace UniversityManagementSystemPortal.RoleManager
{
    public interface IUserRoleManager
    {
        Task AddUserRole(Guid userId, Guid roleId);
        Task RemoveUserRole(Guid userId, Guid roleId);
        Task<IEnumerable<Role>> GetUserRoles(Guid userId);
        Task<IEnumerable<User>> GetUsersInRole(Guid roleId);
    }
    public class UserRoleManager : IUserRoleManager
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        private readonly UmspContext _context;

        public UserRoleManager(UserManager<User> userManager, RoleManager<Role> roleManager, UmspContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task AddUserRole(Guid userId, Guid roleId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var role = await _roleManager.FindByIdAsync(roleId.ToString());

            if (user == null || role == null)
            {
                throw new ArgumentException("User or Role not found.");
            }

            var result = await _userManager.AddToRoleAsync(user, role.Name);

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(",", result.Errors));
            }
        }

        public async Task RemoveUserRole(Guid userId, Guid roleId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var role = await _roleManager.FindByIdAsync(roleId.ToString());

            if (user == null || role == null)
            {
                throw new ArgumentException("User or Role not found.");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(",", result.Errors));
            }
        }

        public async Task<IEnumerable<Role>> GetUserRoles(Guid userId)
        {
            var userRoles = await _context.UserRoles
                .Include(ur => ur.Role)
                .Where(ur => ur.UserId == userId)
                .ToListAsync();

            var roles = userRoles.Select(ur => ur.Role?.Name).ToList();
            return _context.Roles.Where(r => roles.Contains(r.Name)).ToList();
        }
        public async Task<IEnumerable<User>> GetUsersInRole(Guid roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());

            if (role == null)
            {
                throw new ArgumentException("Role not found.");
            }

            return await _userManager.GetUsersInRoleAsync(role.Name);
        }
    }
}
