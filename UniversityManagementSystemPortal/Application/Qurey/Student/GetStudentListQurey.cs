using MediatR;

namespace UniversityManagementSystemPortal.Application.Qurey.Student
{
    public record GetStudentListQurey() : IRequest<List<StudentDto>>;
}
