namespace UniversityManagementSystemPortal.ModelDto.Category
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public string? Prefix { get; set; }
        public Guid InstituteId { get; set; }
        public bool IsActive { get; set; }
        public bool IsStaff { get; set; }
        public bool IsFaculty { get; set; }

    }
}
