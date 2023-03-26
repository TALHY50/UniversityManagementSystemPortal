using MediatR;
using UniversityManagementSystemPortal.Models.ModelDto;

namespace UniversityManagementSystemPortal.Application.Command.UserRoles
{
    public class UpdateUserRoleCommand : IRequest
    {
        public Guid Id { get; set; }
        public CreateUserRoleDto? UserRoleUpdateDto { get; set; }
    }
}
