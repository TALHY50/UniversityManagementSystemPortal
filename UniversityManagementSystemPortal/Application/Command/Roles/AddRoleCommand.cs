using MediatR;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.ModelDto.Role;
using UniversityManagementSystemPortal.Models.ModelDto.Role;

namespace UniversityManagementSystemPortal.Application.Command.Roles
{
    public class AddRoleCommand : IRequest<AddRoleDto>
    {
        public string? Name { get; set; }

        public RoleType RoleType { get; set; }
    }
}
