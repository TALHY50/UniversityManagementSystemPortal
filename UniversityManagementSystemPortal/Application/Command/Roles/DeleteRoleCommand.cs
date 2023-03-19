using MediatR;

namespace UniversityManagementSystemPortal.Application.Command.Roles
{
    public class DeleteRoleCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
