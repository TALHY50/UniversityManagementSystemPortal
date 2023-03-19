using MediatR;

namespace UniversityManagementSystemPortal.Application.Qurey.Student
{
    public class GetStudentByAdmissionNoQuery : IRequest<StudentDto>
    {
        public string AdmissionNo { get; set; }
    }
}
