using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.ModelDto.InstituteDto;
using UniversityManagementSystemPortal.ModelDto.Student;

namespace UniversityManagementSystemPortal
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string AdmissionNo { get; set; } = null!;
        public string RoleNo { get; set; } = null!;
        public string SectionName { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? MobileNo { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; } = null!;

        public StudentCategory Category { get; set; }

        public string? Address { get; set; }
        public bool? IsActive { get; set; }
        public BloodGroup? BloodGroup { get; set; }

        public string ProgramName { get; set; }
        public string? UserName { get; set; }
        public bool? EmailConfirm { get; set; }
        
    }
}
