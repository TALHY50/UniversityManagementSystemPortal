using UniversityManagementSystemPortal.ModelDto.Institute;
using UniversityManagementSystemPortal.Models.ModelDto.Institute;

namespace UniversityManagementSystemPortal.ModelDto.StudentProgram
{
    public class StudentReadDto
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string AdmissionNo { get; set; }
        public int? Category { get; set; }
        public string? Address { get; set; }
        public Guid InstituteId { get; set; }
        public string? ProfilePath { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public InstituteReadDto Institute { get; set; }
        public ICollection<StudentProgramReadDto> StudentPrograms { get; set; }
    }
}
