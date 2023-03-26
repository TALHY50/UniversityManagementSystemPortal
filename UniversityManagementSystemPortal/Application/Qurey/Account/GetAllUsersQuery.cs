using MediatR;
using UniversityManagementSystemPortal.Models.ModelDto.UserDto;

namespace UniversityManagementSystemPortal.Application.Qurey.Account
{
    public class GetAllUsersQuery : IRequest<List<UserViewModel>>
    {
    }
}
