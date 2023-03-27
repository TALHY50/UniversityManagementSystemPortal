using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal.ModelDto.UserRoleDto
{
    public class UserRoleDto
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }

        public Guid? RoleId { get; set; }

        public string? UserName { get; set; }
        public RoleType Roletype { get; set; }

        public DateTime? CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }
    }
}
