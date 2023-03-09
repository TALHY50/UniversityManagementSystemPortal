namespace UniversityManagementSystemPortal.ModelDto.Employee
{
    public class CreateEmployeeDto
    {
        public string EmployeeNo { get; set; } = null!;
        public int? EmployeeType { get; set; }
        public string? Address { get; set; }
        public DateTime? JoiningDate { get; set; }
        public Guid InstituteId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? PositionId { get; set; }
        public Guid? UserId { get; set; }
    }
}
