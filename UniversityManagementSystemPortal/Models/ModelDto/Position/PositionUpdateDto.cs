namespace UniversityManagementSystemPortal.ModelDto.Position
{
    public class PositionAddorUpdateDto
    {
        public string Name { get; set; }
        public Guid? CategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}
