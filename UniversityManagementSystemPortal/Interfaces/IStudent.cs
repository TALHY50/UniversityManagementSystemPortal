using UniversityManagementSystemPortal.Helpers.Paging;
using UniversityManagementSystemPortal.Models.ModelDto.Student;

namespace UniversityManagementSystemPortal.Interfaces
{
    public interface IStudentRepository
    {
        IQueryable<Student> Get();
        Task<Student> GetById(Guid id);
        Task<Student> Add(Student student);
        //Student AddBulk(Student student, User user, StudentReadModel dto)
        Task<Student> Update(Student student);
        Task Delete(Guid id);
        Task<Student> GetByAdmissionNo(string admissionNo);
        Task<List<string>> Upload(List<StudentReadModel> studentDataList);
    }
}
