using UniversityManagementsystem.Models;

namespace UniversityManagementSystemPortal.Interfaces
{
    public interface IInstituteAdminRepository
    {
        Task<InstituteAdmin> GetByIdAsync(Guid id);
        Task<InstituteAdmin> GetByUserIdAsync(Guid userId);
        Task AddAsync(InstituteAdmin instituteAdmin);
        Task UpdateAsync(InstituteAdmin instituteAdmin);
        Task DeleteAsync(InstituteAdmin instituteAdmin);
        Task<IEnumerable<InstituteAdmin>> GetInstituteAdminsAsync(Guid instituteId);
        Task<Guid?> GetInstituteIdByActiveUserId(Guid activeUserId);
    }

}
