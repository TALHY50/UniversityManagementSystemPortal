using MediatR;
using UniversityManagementSystemPortal.ModelDto.NewFolder;
using UniversityManagementSystemPortal.Models.ModelDto.UserDto;

namespace UniversityManagementSystemPortal
{
    public class LoginCommand : IRequest<LoginView>
    {
        public Login model { get; set; }
    }

}
