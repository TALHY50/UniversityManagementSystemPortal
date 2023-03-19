using MediatR;

namespace UniversityManagementSystemPortal.Application.Command.Student
{
    public class DeleteStudentCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
