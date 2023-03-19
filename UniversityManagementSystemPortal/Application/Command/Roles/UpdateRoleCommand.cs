using MediatR;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.ModelDto.Role;

namespace UniversityManagementSystemPortal.Application.Command.Roles
{
    public class UpdateRoleCommand : IRequest<UpdateRoleDto>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public RoleType RoleType { get; set; }
    }
}
