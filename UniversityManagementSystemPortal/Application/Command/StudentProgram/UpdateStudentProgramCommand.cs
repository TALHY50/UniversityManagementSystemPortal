using MediatR;
using UniversityManagementSystemPortal.ModelDto.StudentProgram;

namespace UniversityManagementSystemPortal.Application.Command.StudentProgram
{
    public class UpdateStudentProgramCommand : IRequest
    {
        public Guid Id { get; set; }
        public StudentProgramUpdateDto StudentProgramUpdateDto { get; set; }

        public UpdateStudentProgramCommand(Guid id, StudentProgramUpdateDto studentProgramUpdateDto)
        {
            Id = id;
            StudentProgramUpdateDto = studentProgramUpdateDto;
        }

    }
}
