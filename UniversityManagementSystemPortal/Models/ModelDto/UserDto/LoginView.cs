

using System.Text.Json.Serialization;

namespace UniversityManagementSystemPortal.ModelDto.NewFolder
{
    public class LoginView
    {

        
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public LoginView(User user, string token)
        {
            Token = token;
            Username = user.Username;
            Email = user.Email;
        }
    }

}
