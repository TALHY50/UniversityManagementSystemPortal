using MediatR;
using UniversityManagementSystemPortal.ModelDto.StudentProgram;

namespace UniversityManagementSystemPortal.Application.Qurey.StudentProgram
{
    public class GetAllStudentProgramsQuery : IRequest<List<StudentProgramDto>>
    {
    }
}
