using System.Threading.Tasks;

namespace UniversityManagementSystemPortal.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> GetByIdAsync(Guid id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<IEnumerable<Category>> GetByInstituteIdAsync(Guid instituteId);
        Task<Category> AddAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task DeleteAsync(Category category);
        Task<Category> GetCategoryByName(string categoryName);
        Task<bool> SaveChangesAsync();
    }
}
