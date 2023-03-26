using MediatR;
using UniversityManagementSystemPortal.ModelDto.StudentProgram;
using UniversityManagementSystemPortal.Models.ModelDto.StudentProgram;

namespace UniversityManagementSystemPortal.Application.Qurey.StudentProgram
{
    public class GetStudentProgramByIdQuery : IRequest<StudentProgramDto>
    {
        public Guid Id { get; set; }

    }
}
