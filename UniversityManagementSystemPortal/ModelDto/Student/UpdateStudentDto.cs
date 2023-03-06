namespace UniversityManagementSystemPortal.ModelDto.Student
{
    public class UpdateStudentDto
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string AdmissionNo { get; set; }
        public int? Category { get; set; }
        public string Address { get; set; }
        public Guid InstituteId { get; set; }
        public string ProfilePath { get; set; }
        public bool IsActive { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
