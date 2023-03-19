using MediatR;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.ModelDto.Role;

namespace UniversityManagementSystemPortal
{
    public class AddRoleCommand : IRequest<AddRoleDto>
    {
        public string? Name { get; set; }

        public RoleType RoleType { get; set; }
    }
}
