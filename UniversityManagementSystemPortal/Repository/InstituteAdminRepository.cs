using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Models.DbContext;

namespace UniversityManagementSystemPortal.Repository
{
    public class InstituteAdminRepository : IInstituteAdminRepository
    {
        private readonly IIdentityServices _identityService;
        private readonly UmspContext _context;

        public InstituteAdminRepository(UmspContext context, IIdentityServices identityService)
        {
            _context = context;
            _identityService = identityService;
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
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(InstituteAdmin instituteAdmin)
        {
            _context.InstituteAdmins.Update(instituteAdmin);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(InstituteAdmin instituteAdmin)
        {
            _context.InstituteAdmins.Remove(instituteAdmin);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<InstituteAdmin>> GetInstituteAdminsAsync(Guid instituteId)
        {
            return await _context.InstituteAdmins.AsNoTracking()
                .Include(i => i.User)
                .Where(i => i.InstituteId == instituteId)
                .ToListAsync();
        }
        public async Task<Guid?> GetInstituteIdByActiveUserId(Guid activeUserId)
        {
            var instituteAdmin = await _context.InstituteAdmins
                .FirstOrDefaultAsync(i => i.UserId == activeUserId);

            if (instituteAdmin == null)
            {
                return null;
            }

            return instituteAdmin.InstituteId;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
