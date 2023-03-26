using MediatR;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.ModelDto.UserDto;
using UniversityManagementSystemPortal.Models;
using UniversityManagementSystemPortal.Models.ModelDto.UserDto;

namespace UniversityManagementSystemPortal.Application.Command.Account
{
    public class RegisterUserCommand : IRequest<RegistorUserDto>
    {
        public RegistorUserDto RegisterUserDto { get; set; }
    }
}
