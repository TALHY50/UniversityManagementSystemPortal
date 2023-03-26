using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal.ModelDto.UserRoleDto
{
    public class UserRoleDto
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }

        public Guid? RoleId { get; set; }

        public string? UserName { get; set; }
        public string Roletype { get; set; }
    }
}
