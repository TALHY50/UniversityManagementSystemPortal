using MediatR;

namespace UniversityManagementSystemPortal.Application.Command.Program
{
    public class DeleteProgramCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
