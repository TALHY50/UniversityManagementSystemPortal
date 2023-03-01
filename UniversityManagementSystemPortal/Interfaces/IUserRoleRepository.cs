using UniversityManagementsystem.Models;

namespace UniversityManagementSystemPortal.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<UserRole> GetByIdAsync(Guid id);
        Task<IEnumerable<UserRole>> GetAllAsync();
        Task<UserRole> CreateAsync(UserRole userRole);
        Task UpdateAsync(UserRole userRole);
        Task DeleteAsync(UserRole userRole);
    }
}
