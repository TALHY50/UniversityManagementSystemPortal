using UniversityManagementsystem.Models;

namespace UniversityManagementSystemPortal.Interfaces
{
    public interface IUserRoleInterface
    {
        Task AddUserRoleAsync(Guid roleId, Guid userId);
        Task RemoveUserRoleAsync(Guid roleId, Guid userId);
        Task<UserRole> GetByIdAsync(Guid id);
        Task<IEnumerable<UserRole>> GetAllAsync();
        Task UpdateAsync(UserRole userRole);
    }

}
