using MediatR;
using UniversityManagementSystemPortal.ModelDto.Role;
using UniversityManagementSystemPortal.Models.ModelDto.Role;

namespace UniversityManagementSystemPortal.Application.Qurey.Roles
{
    public class GetRoleByIdQuery : IRequest<RoleDto>
    {
        public Guid Id { get; set; }
    }
}
