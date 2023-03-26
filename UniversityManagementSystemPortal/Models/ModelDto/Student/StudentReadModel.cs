using CsvHelper.Configuration;
using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal.Models.ModelDto.Student
{
    public class StudentReadModel
    {
        public string AdmissionNo { get; set; } = null!;
        public string RoleNo { get; set; } = null!;
        public string? FirstName { get; set; } = null!;

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

        public string? ProgramName { get; set; }
        public string? UserName { get; set; }
        public bool? EmailConfirm { get; set; }
        public string? Password { get; set; }

    }
    public class StudentReadModelMap : ClassMap<StudentReadModel>
    {
        public StudentReadModelMap()
        {
            Map(s => s.AdmissionNo).Name("Admission No");
            Map(s => s.RoleNo).Name("Role No");
            Map(s => s.FirstName).Name("First Name");
            Map(s => s.MiddleName).Name("Middle Name");
            Map(s => s.LastName).Name("Last Name");
            Map(s => s.MobileNo).Name("Mobile No");
            Map(s => s.DateOfBirth).Name("Date of Birth");
            Map(s => s.Gender).Name("Gender");
            Map(s => s.Email).Name("Email");
            Map(s => s.Category).Name("Category");
            Map(s => s.Address).Name("Address");
            Map(s => s.IsActive).Name("Is Active");
            Map(s => s.BloodGroup).Name("Blood Group");
            Map(s => s.ProgramName).Name("Program Name");
            Map(s => s.UserName).Name("User Name");
            Map(s => s.EmailConfirm).Name("Email Confirm");
            Map(s => s.Password).Name("Password");
        }

    }
}
