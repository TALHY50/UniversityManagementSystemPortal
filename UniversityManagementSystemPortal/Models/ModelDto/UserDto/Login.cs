using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystemPortal.ModelDto.NewFolder
{
    public class Login
    {
        public Guid UserId { get; set; }    
        public string? Email { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }

}
