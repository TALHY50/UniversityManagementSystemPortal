using MediatR;
using UniversityManagementSystemPortal.ModelDto.StudentProgram;
using UniversityManagementSystemPortal.Models.ModelDto.StudentProgram;

namespace UniversityManagementSystemPortal.Application.Command.StudentProgram
{
    public class AddStudentProgramCommand : IRequest<StudentProgramDto>
    {
        public StudentProgramCreateDto? StudentProgramCreateDto { get; set; }
    }

}
