using MediatR;

namespace UniversityManagementSystemPortal.Application.Command.Institute
{
    public class DeleteInstituteCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
