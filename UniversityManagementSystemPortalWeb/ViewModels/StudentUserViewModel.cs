using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.ModelDto.UserDto;
using UniversityManagementSystemPortal.Models.ModelDto.Student;
namespace UniversityManagementSystemPortalWeb.ViewModels
{
    public class StudentUserViewModel
    {
        public AddStudentDto studentDto { get; set; } = new AddStudentDto();
        public RegistorUserDto userDto { get; set; } = new RegistorUserDto();
    }

    public class AddStudentDto
    {
        public Guid? UserId { get; set; }
        public string AdmissionNo { get; set; }
        public StudentCategory? Category { get; set; }
        public string Address { get; set; }
        public IFormFile? Picture { get; set; }
    }

    public class RegistorUserDto
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? FirstName { get; set; }

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

        public string? Password { get; set; } 
    }
}
