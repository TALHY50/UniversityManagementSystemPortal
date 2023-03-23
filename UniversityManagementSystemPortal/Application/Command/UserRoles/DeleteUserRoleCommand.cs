using MediatR;

namespace UniversityManagementSystemPortal.Application.Command.UserRoles
{
    public class DeleteUserRoleCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
