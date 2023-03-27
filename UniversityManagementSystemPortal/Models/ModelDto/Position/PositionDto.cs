namespace UniversityManagementSystemPortal.ModelDto.Position
{
    public class PositionDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public Guid? CategoryId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
    }

}
