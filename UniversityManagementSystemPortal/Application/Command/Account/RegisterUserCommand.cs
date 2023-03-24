using MediatR;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.ModelDto.UserDto;
using UniversityManagementSystemPortal.Models;

namespace UniversityManagementSystemPortal.Application.Command.Account
{
    public class RegisterUserCommand : IRequest<RegistorUserDto>
    {
        public RegistorUserDto RegisterUserDto { get; set; }
    }
}
