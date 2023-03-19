using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal
{
    public class UpdateStudentDto
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string AdmissionNo { get; set; }
        public StudentCategory? Category { get; set; }
        public string Address { get; set; }
        public Guid InstituteId { get; set; }
        public string ProfilePath { get; set; }
        public bool IsActive { get; set; }

    }
}
