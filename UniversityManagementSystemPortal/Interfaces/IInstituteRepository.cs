

namespace UniversityManagementSystemPortal.Interfaces
{
    public interface IInstituteRepository
    {
        Task<Institute> GetByIdAsync(Guid id);
        IQueryable<Institute> GetAllAsync();
        Task AddAsync(Institute institute);
        Task UpdateAsync(Institute institute);

        Task DeleteAsync(Guid id);
        Task<bool> SaveChangesAsync();

    }
}
