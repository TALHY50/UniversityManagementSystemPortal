using MediatR;
using UniversityManagementSystemPortal.ModelDto.UserRoleDto;
using UniversityManagementSystemPortal.Models.ModelDto;

namespace UniversityManagementSystemPortal.Application.Command.UserRoles
{
    public class CreateUserCommand : IRequest<UserRoleDto>
    {
        public CreateUserRoleDto CreateUserRoleDto { get; set; }
    }
}
