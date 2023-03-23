using MediatR;
using UniversityManagementSystemPortal.ModelDto.UserRoleDto;

namespace UniversityManagementSystemPortal.Application.Command.UserRoles
{
    public class CreateUserCommand : IRequest<UserRoleDto>
    {
        public CreateUserRoleDto CreateUserRoleDto { get; set; }
    }
}
