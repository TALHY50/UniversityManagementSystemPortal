using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal.ModelDto.Role
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public RoleType RoleType { get; set; }
        public DateTime? CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }
    }
}
