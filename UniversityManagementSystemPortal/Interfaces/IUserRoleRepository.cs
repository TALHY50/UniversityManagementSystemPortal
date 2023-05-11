
namespace UniversityManagementSystemPortal.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<UserRole> GetByIdAsync(Guid id);
        Task<IEnumerable<UserRole>> GetAllAsync();
        Task AddAsync(UserRole userRole);
        Task UpdateAsync(UserRole userRole);
        Task DeleteAsync(UserRole userRole);
        Task<UserRole> GetByRoleNameAsync(string roleName);
        Task<bool> SaveChangesAsync();

    }

}
