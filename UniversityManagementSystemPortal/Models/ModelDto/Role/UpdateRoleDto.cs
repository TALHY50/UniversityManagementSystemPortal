using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal.Models.ModelDto.Role
{
    public class UpdateRoleDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }

        public RoleType RoleType { get; set; }
    }
}
