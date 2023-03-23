using UniversityManagementsystem.Models;

namespace UniversityManagementSystemPortal.Interfaces
{
    public interface IPositionRepository
    {
        Task<IEnumerable<Position>> GetByIdAsync(Guid id, Guid? instituteId = null);
        Task<IEnumerable<Position>> GetAllAsync(Guid? instituteId = null);
        Task<IEnumerable<Position>> GetByCategoryIdAsync(Guid categoryId);
        Task CreateAsync(Position position);
        Task UpdateAsync(Position position, Guid? instituteId = null);
        Task DeleteAsync(Guid id);
        Task<Position> GetPositionByName(string positionName);
        Task<IEnumerable<Position>> GetByInstituteIdAsync(Guid? instituteId = null);
        Task<bool> SaveChangesAsync();
    }


}
