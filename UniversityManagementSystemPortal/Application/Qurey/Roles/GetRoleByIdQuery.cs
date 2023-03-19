using MediatR;
using UniversityManagementSystemPortal.ModelDto.Role;

namespace UniversityManagementSystemPortal.Application.Qurey.Roles
{
    public class GetRoleByIdQuery : IRequest<RoleDto>
    {
        public Guid Id { get; set; }
    }
}
