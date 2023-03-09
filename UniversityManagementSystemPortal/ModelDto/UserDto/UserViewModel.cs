using System.ComponentModel.DataAnnotations;
using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal.ModelDto.UserDto
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The first name field is required.")]
        [StringLength(50, ErrorMessage = "The first name must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; } = null!;

        [StringLength(50, ErrorMessage = "The middle name cannot be longer than {1} characters.")]
        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "The last name field is required.")]
        [StringLength(50, ErrorMessage = "The last name must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string? LastName { get; set; }

        [RegularExpression(@"^(?:\+88|01)?(?:\d{11}|\d{13})$", ErrorMessage = "Invalid mobile number.")]
        public string? MobileNo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "The blood group field is required.")]
        [Display(Name = "Blood Group")]
        [EnumDataType(typeof(BloodGroup))]
        public BloodGroup BloodGroup { get; set; }

        [Required(ErrorMessage = "The gender field is required.")]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "The email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "The username field is required.")]
        [StringLength(50, ErrorMessage = "The username must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "The password field is required.")]
        [StringLength(50, ErrorMessage = "The password must be between {2} and {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public bool IsActive { get; set; }

        public DateTime? LastLoggedIn { get; set; }

        public DateTime? CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }
    }
}
