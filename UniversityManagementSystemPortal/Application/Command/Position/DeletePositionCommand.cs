using MediatR;

namespace UniversityManagementSystemPortal.Application.Command.Position
{
    public class DeletePositionCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid? InstituteId { get; set; }
    }
}
