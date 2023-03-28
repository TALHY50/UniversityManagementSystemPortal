using MediatR;
using System.ComponentModel.DataAnnotations;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.ModelDto.UserDto;
using UniversityManagementSystemPortal.Models;
using UniversityManagementSystemPortal.Models.ModelDto.UserDto;

namespace UniversityManagementSystemPortal.Application.Command.Account
{
    public class RegisterUserCommand : IRequest<RegistorUserDto>
    {
        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? MobileNo { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "The blood group field is required.")]
        [EnumDataType(typeof(BloodGroup))]
        public BloodGroup BloodGroup { get; set; }

        [Required(ErrorMessage = "The gender field is required.")]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        public string? Email { get; set; } 

        public string? Username { get; set; }

        public string Password { get; set; } 
    }
}
