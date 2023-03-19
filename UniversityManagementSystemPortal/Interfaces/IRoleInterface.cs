using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal.Interfaces
{
    public interface IRoleInterface
    {
        Task<Role> GetByIdAsync(Guid id);
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role> CreateAsync(Role role);
        Task UpdateAsync(Role role);
        Task DeleteAsync(Role role);
        Task<Role> GetByRoleTypeAsync(RoleType roleType);
    }
}
