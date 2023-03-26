using MediatR;
using UniversityManagementSystemPortal.ModelDto.Role;
using UniversityManagementSystemPortal.Models.ModelDto.Role;

namespace UniversityManagementSystemPortal.Application.Qurey.Roles
{
    public class GetAllRolesQuery : IRequest<IEnumerable<RoleDto>>
    {
    }

}
