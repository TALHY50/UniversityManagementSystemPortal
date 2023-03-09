namespace UniversityManagementSystemPortal.ModelDto.Program
{
    public class ProgramUpdateDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string SectionName { get; set; }
        public int GradingType { get; set; }
        public Guid DepartmentId { get; set; }
    }
}