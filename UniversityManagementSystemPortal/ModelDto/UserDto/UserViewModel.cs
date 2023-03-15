using System.ComponentModel.DataAnnotations;
using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal.ModelDto.UserDto
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

      

        public string FirstName { get; set; } = null!;

     

        public string? MiddleName { get; set; }


        public string? LastName { get; set; }


        public string? MobileNo { get; set; }

      
        public DateTime? DateOfBirth { get; set; }

        public BloodGroup BloodGroup { get; set; }

        public Gender Gender { get; set; }

      
        public string Email { get; set; } = null!;

       
        public string Username { get; set; } = null!;

       
        public string Password { get; set; } = null!;

        public bool IsActive { get; set; }

        public DateTime? LastLoggedIn { get; set; }

        public DateTime? CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }
    }
}
