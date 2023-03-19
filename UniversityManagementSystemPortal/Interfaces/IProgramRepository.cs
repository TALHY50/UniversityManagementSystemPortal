using UniversityManagementsystem.Models;
using PorgramNamespace = UniversityManagementsystem.Models.Program;

namespace UniversityManagementSystemPortal
{
    public interface IProgramRepository
    {
        Task<PorgramNamespace> GetByIdAsync(Guid id);
        Task<IEnumerable<PorgramNamespace>> GetAllAsync();
        Task AddAsync(PorgramNamespace program);
        Task UpdateAsync(PorgramNamespace program);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<StudentProgram>> GetStudentProgramsAsync(Guid programId);
        Task<PorgramNamespace> GetProgramByName(string programName);
    }
}
