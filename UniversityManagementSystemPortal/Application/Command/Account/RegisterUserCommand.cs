using MediatR;
using System.ComponentModel.DataAnnotations;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.ModelDto.UserDto;
using UniversityManagementSystemPortal.Models;
using UniversityManagementSystemPortal.Models.ModelDto.UserDto;

namespace UniversityManagementSystemPortal.Application.Command.Account
{
    public record RegisterUserCommand (RegistorUserDto registorUserDto): IRequest<RegistorUserDto>;
    
}
