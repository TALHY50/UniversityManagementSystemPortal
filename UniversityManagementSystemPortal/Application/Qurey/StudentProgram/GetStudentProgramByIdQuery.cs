using MediatR;
using UniversityManagementSystemPortal.ModelDto.StudentProgram;

namespace UniversityManagementSystemPortal.Application.Qurey.StudentProgram
{
    public class GetStudentProgramByIdQuery : IRequest<StudentProgramDto>
    {
        public Guid Id { get; set; }

    }
}
