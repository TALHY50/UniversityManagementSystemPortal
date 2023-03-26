using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal.ModelDto.Employee
{
    public class EmployeeReadDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } 

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? MobileNo { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public BloodGroup BloodGroup { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string EmployeeNo { get; set; }
        public string Address { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public string? EmployeAddress { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string? InstituteName { get; set; }
        public string? InstituteType { get; set; }
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
