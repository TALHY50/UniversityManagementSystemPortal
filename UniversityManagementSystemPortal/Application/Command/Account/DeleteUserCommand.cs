using MediatR;

namespace UniversityManagementSystemPortal.Application.Command.Account
{
    public class DeleteUserCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}
