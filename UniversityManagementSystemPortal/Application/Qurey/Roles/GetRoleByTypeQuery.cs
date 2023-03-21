using MediatR;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.ModelDto.Role;

namespace UniversityManagementSystemPortal.Application.Qurey.Roles
{
    public class GetRoleByTypeQuery : IRequest<RoleDto>
    {
        public RoleType RoleType { get; set; }
    }
}
