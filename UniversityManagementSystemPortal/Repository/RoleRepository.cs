﻿using Microsoft.EntityFrameworkCore;
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
            if (role == null)
            {
                return null;
            }
            return role;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            var roles = await _context.Roles.ToListAsync();

            if (roles == null)
            {
                return null;
            }

            return roles;
        }

        public async Task<Role> CreateAsync(Role role)
        {
            if (role == null)
            {
                return null;
            }

            role.Id = Guid.NewGuid();
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task UpdateAsync(Role role)
        {
            if (role == null)
            {
                return;
            }

            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Role role)
        {
            if (role == null)
            {
                return;
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }

        public async Task<Role> GetByRoleTypeAsync(int roleType)
        {
            if (roleType == null)
            {
                return null;
            }

            var role = await _context.Roles
                .Include(r => r.UserRoles)
                .ThenInclude(ur => ur.User)
                .FirstOrDefaultAsync(r => r.RoleType.HasValue);

            return role;
        }
    }

}
