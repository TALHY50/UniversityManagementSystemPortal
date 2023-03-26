using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal.ModelDto.Employee
{
    public class EmployeeReadModel
    {
        public string? EmployeeNo { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? MobileNo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public BloodGroup BloodGroup { get; set; }
        public Gender Gender { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string? DepartmentName { get; set; }
        public string? PositionName { get; set; }
        public string? EmployeAddress { get; set; }

    }
}
