using System.ComponentModel.DataAnnotations;
using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal.ModelDto.UserDto
{
    public class RegistorViewModel
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

        public string Email { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }
    }

}
