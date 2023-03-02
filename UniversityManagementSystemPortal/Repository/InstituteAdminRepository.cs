using Microsoft.AspNetCore.Http.HttpResults;
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
            return await _context.InstituteAdmins
                .Include(i => i.Institute)
                .Include(i => i.User)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<InstituteAdmin> GetByUserIdAsync(Guid userId)
        {
            return await _context.InstituteAdmins
                .Include(i => i.Institute)
                .Include(i => i.User)
                .FirstOrDefaultAsync(i => i.UserId == userId);
        }

        public async Task<bool> IsSuperAdminAsync(Guid userId)
        {
            return await _context.InstituteAdmins
                .AnyAsync(i => i.UserId == userId && i.InstituteId == null);
        }

        public async Task<bool> IsAdminAsync(Guid userId, Guid instituteId)
        {
            return await _context.InstituteAdmins
                .AnyAsync(i => i.UserId == userId && i.InstituteId == instituteId);
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
            return await _context.InstituteAdmins
                .Include(i => i.User)
                .Where(i => i.InstituteId == instituteId)
                .ToListAsync();
        }
    }




}
