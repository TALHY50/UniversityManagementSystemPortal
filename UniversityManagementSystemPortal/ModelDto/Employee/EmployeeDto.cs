namespace UniversityManagementSystemPortal.ModelDto.Employee
{
    public class EmployeeDto

    {
        public Guid Id { get; set; }
           public string EmployeeNo { get; set; }
             public int? EmployeeType { get; set; }
                  public string? Address { get; set; }
             public DateTime? JoiningDate { get; set; }
         public Guid InstituteId { get; set; }
         public string? ProfilePath { get; set; }
         public Guid? DepartmentId { get; set; }
             public Guid? PositionId { get; set; }
         public bool IsActive { get; set; }
    }
}
