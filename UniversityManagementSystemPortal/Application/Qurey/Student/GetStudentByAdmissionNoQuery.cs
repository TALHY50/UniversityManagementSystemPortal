using MediatR;
using UniversityManagementSystemPortal.Models.ModelDto.Student;

namespace UniversityManagementSystemPortal.Application.Qurey.Student
{
    public class GetStudentByAdmissionNoQuery : IRequest<StudentDto>
    {
        public string AdmissionNo { get; set; }
    }
}
