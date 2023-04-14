using MediatR;
using UniversityManagementSystemPortal.ModelDto.NewFolder;
using UniversityManagementSystemPortal.Models.ModelDto.UserDto;

namespace UniversityManagementSystemPortal
{
    public record LoginCommand(Login model) : IRequest<LoginView>;
   
    

}
