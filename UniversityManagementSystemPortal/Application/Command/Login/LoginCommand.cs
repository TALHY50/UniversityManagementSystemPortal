using MediatR;
using UniversityManagementSystemPortal.ModelDto.NewFolder;

namespace UniversityManagementSystemPortal.Application.Command.Account
{
    public class LoginCommand : IRequest<LoginView>
    {
        public Login model { get; set; }
    }

}
