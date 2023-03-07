using System.Text.Json.Serialization;
using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal.CsvImport.dtomodels
{
    public class CsvStudentDto
    {
        public string AdmissionNo { get; set; } = null!;
        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? MobileNo { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; } = null!;

        public StudentCategory Category { get; set; }

        public string? Address { get; set; }

    }
}
