using MediatR;
using UniversityManagementSystemPortal.ModelDto.UserRoleDto;

namespace UniversityManagementSystemPortal.Application.Qurey.UserRoles
{

    public class GetAllUserRolesQuery : IRequest<List<UserRoleDto>>
    {
    }
}
