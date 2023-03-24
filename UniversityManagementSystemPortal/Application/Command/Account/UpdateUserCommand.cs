using MediatR;
using UniversityManagementSystemPortal.ModelDto.UserDto;

namespace UniversityManagementSystemPortal.Application.Command.Account
{
    public class UpdateUserCommand : IRequest<UserViewModel>
    {
        public Guid Id { get; set; }
        public UpdateUserDto UpdateUserDto { get; set; }
    }
}
