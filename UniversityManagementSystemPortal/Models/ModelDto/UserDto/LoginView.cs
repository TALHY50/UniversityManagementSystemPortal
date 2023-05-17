using System.Drawing;

namespace UniversityManagementSystemPortal.ModelDto.NewFolder
{
    public class LoginView
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Roles { get; set; }

        public LoginView() { } // Add a parameterless constructor

        public LoginView(User user, string token)
        {
            Roles = string.Join(", ", user.UserRoles.Where(ur => ur.UserId == user.Id).Select(ur => ur.Role.Name));
            UserId = user.Id;
            Token = token;
            Username = user.Username;
            Email = user.Email;
        }


    }
}

