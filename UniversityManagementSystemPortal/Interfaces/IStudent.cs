using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Helpers.Paging;
using UniversityManagementSystemPortal.ModelDto.Student;

namespace UniversityManagementSystemPortal.Interfaces
{
    public interface IStudentRepository
    {
        Task<PaginatedList<Student>> Get(PaginatedViewModel paginatedViewModel);
        Task<Student> GetById(Guid id);
        Task<Student> Add(Student student);
        Student AddBulk(Student student, User user, StudentReadModel dto);
        Task<StudentReadModel> AddToImport(StudentReadModel student);
        Task<Student> Update(Student student);
        Task Delete(Guid id);
        Task<Student> GetByAdmissionNo(string admissionNo);
        //List<string> Upload(List<StudentReadModel> studentDataList);
    }
}
