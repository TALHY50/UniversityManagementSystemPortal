using System.Threading.Tasks;
using UniversityManagementsystem.Models;

namespace UniversityManagementSystemPortal.Interfaces
{
    public interface IPositionRepository
    {
        Task<Position> GetByIdAsync(Guid id);
        Task<IEnumerable<Position>> GetAllAsync();
        Task<IEnumerable<Position>> GetByCategoryIdAsync(Guid categoryId);
        Task CreateAsync(Position position);
        Task UpdateAsync(Position position);
        Task DeleteAsync(Guid id);
    }
}
