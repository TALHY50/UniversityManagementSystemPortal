using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal.ModelDto.Employee
{
    public class EmployeeDto

    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? MobileNo { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public int? BloodGroup { get; set; }
        public Gender Gender { get; set; }
        public string EmployeeNo { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public string? EmployeAddress { get; set; }
        public DateTime? JoiningDate { get; set; }
        public Guid InstituteId { get; set; }
        public string? InstituteName { get; set; }
        public string? ProfilePath { get; set; }
        public string? DepartmentName { get; set; }
        public string? DepartmentCode { get; set; }
        public string PositionName { get; set; } = null!;

        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }
    }
}
