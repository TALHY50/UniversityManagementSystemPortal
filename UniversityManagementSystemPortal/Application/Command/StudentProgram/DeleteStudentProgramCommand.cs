using MediatR;

namespace UniversityManagementSystemPortal.Application.Command.StudentProgram
{
    public class DeleteStudentProgramCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
