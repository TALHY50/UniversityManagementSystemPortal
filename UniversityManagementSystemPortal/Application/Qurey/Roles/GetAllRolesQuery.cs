using MediatR;
using UniversityManagementSystemPortal.ModelDto.Role;

namespace UniversityManagementSystemPortal.Application.Qurey.Roles
{
    public class GetAllRolesQuery : IRequest<IEnumerable<RoleDto>>
    {
    }

}
