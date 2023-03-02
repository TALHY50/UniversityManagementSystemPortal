using UniversityManagementsystem.Models;

namespace UniversityManagementSystemPortal.Interfaces
{
    public interface IInstituteAdminRepository
    {
        Task<InstituteAdmin> GetByIdAsync(Guid id);
        Task<InstituteAdmin> GetByUserIdAsync(Guid userId);
        Task<bool> IsSuperAdminAsync(Guid userId);
        Task<bool> IsAdminAsync(Guid userId, Guid instituteId);
        Task AddAsync(InstituteAdmin instituteAdmin);
        Task UpdateAsync(InstituteAdmin instituteAdmin);
        Task DeleteAsync(InstituteAdmin instituteAdmin);
        Task<IEnumerable<InstituteAdmin>> GetInstituteAdminsAsync(Guid instituteId);
    }

}
