using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystemPortal.ModelDto.NewFolder
{
    public class Login
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
