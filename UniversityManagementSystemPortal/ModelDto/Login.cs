using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystemPortal.ModelDto
{
    public class Login
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
