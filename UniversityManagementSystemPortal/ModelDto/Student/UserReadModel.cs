using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal.ModelDto.Student
{
    public class UserReadModel
    {
        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? MobileNo { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; } = null!;

        public string Username { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
