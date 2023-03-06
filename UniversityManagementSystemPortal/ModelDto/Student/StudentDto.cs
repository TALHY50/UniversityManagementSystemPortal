using UniversityManagementSystemPortal.ModelDto.InstituteDto;

namespace UniversityManagementSystemPortal
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string AdmissionNo { get; set; }
        public int? Category { get; set; }
        public string? Address { get; set; }
        public string? ProfilePath { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid InstituteId { get; set; }
        public InstituteDto Institute { get; set; }

    }
}
