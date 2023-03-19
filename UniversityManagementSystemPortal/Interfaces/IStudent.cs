using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.ModelDto.Student;

namespace UniversityManagementSystemPortal.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<Student>> Get();
        Task<Student> GetById(Guid id);
        Task<Student> Add(Student student);
        Student AddBulk(Student student, User user, StudentReadModel dto);
        Task<StudentReadModel> AddToImport(StudentReadModel student);
        Task<Student> Update(Student student);
        Task Delete(Guid id);
        Task<Student> GetByAdmissionNo(string admissionNo);
        (string message, List<string> skippedEntries) Upload(IFormFile file);
    }
}
