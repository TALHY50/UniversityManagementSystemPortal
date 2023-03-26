using Microsoft.Build.Framework;

namespace UniversityManagementSystemPortal
{
    public partial class UserRole
    {

        public Guid Id { get; set; }
        [Required]
        public Guid? UserId { get; set; }
        [Required]
        public Guid? RoleId { get; set; }

        public virtual Role? Role { get; set; }

        public virtual User? User { get; set; }
    }
}