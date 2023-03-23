using MediatR;
using UniversityManagementSystemPortal.ModelDto.UserRoleDto;

namespace UniversityManagementSystemPortal.Application.Command.UserRoles
{
    public class UpdateUserRoleCommand : IRequest
    {
        public Guid Id { get; set; }
        public CreateUserRoleDto? UserRoleUpdateDto { get; set; }
    }
}
