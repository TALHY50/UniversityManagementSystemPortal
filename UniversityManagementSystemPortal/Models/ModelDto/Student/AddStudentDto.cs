using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal.Models.ModelDto.Student
{
    public class AddStudentDto
    {
        public Guid? UserId { get; set; }
        public string AdmissionNo { get; set; }
        public StudentCategory? Category { get; set; }
        public string Address { get; set; }
        public IFormFile? Picture { get; set; }

    }
}
