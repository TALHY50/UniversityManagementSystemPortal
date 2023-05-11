using UniversityManagementSystemPortal.ModelDto.NewFolder;

namespace UniversityManagementSystemPortal.Models.ModelDto.UserDto
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public LoginView LoginView { get; set; }
    }
}
