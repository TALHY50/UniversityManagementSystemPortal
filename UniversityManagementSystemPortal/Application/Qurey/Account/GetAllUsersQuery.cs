using MediatR;
using UniversityManagementSystemPortal.ModelDto.UserDto;

namespace UniversityManagementSystemPortal.Application.Qurey.Account
{
    public class GetAllUsersQuery : IRequest<List<UserViewModel>>
    {
    }
}
