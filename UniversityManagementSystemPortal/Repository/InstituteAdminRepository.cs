﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Repository
{
    public class InstituteAdminRepository : IInstituteAdminRepository
    {
        private readonly UmspContext _context;

        public InstituteAdminRepository(UmspContext context)
        {
            _context = context;
        }

        public async Task<InstituteAdmin> GetByIdAsync(Guid id)
        {
            var instituteAdmin = await _context.InstituteAdmins
                .Include(i => i.Institute)
                .Include(i => i.User)
                .SingleOrDefaultAsync(i => i.Id == id);

            return instituteAdmin;
        }

        public async Task<InstituteAdmin> GetByUserIdAsync(Guid userId)
        {
            var instituteAdmin = await _context.InstituteAdmins
                .Include(i => i.Institute)
                .Include(i => i.User)
                .SingleOrDefaultAsync(i => i.UserId == userId);

            return instituteAdmin;
        }

        public async Task AddAsync(InstituteAdmin instituteAdmin)
        {
            await _context.InstituteAdmins.AddAsync(instituteAdmin);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(InstituteAdmin instituteAdmin)
        {
            _context.InstituteAdmins.Update(instituteAdmin);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(InstituteAdmin instituteAdmin)
        {
            _context.InstituteAdmins.Remove(instituteAdmin);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<InstituteAdmin>> GetInstituteAdminsAsync(Guid instituteId)
        {
            return await _context.InstituteAdmins.AsNoTracking()
                .Include(i => i.User)
                .Where(i => i.InstituteId == instituteId)
                .ToListAsync();
        }
    }





}
