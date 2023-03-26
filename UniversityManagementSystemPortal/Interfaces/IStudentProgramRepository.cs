
namespace UniversityManagementSystemPortal.Interfaces
{
    public interface IStudentProgramRepository
    {
        Task AddStudentProgramAsync(StudentProgram studentProgram);

        Task<IEnumerable<StudentProgram>> GetAllStudentProgramsAsync();

        Task<StudentProgram> GetStudentProgramByIdAsync(Guid id);

        Task UpdateStudentProgramAsync(StudentProgram studentProgram);

        Task DeleteStudentProgramAsync(Guid id);
        Task<bool> SaveChangesAsync();
    }
}
