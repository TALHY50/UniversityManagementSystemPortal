namespace UniversityManagementSystemPortal.ModelDto.Department
{
    public class DepartmentCreateDto
    {
    
        public string? Code { get; set; }
        public string? Name { get; set; }
        public Guid InstituteId { get; set; }
        public bool IsActive { get; set; }
        public bool IsAcademics { get; set; }
        public bool IsAdministrative { get; set; }
    }
}
