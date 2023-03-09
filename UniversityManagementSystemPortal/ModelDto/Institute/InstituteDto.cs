using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal.ModelDto.InstituteDto
{
    public class InstituteDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public InstituteType Type { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string? Website { get; set; }
        public string? Address { get; set; }
        public bool IsAutoIncrementAdmissionNo { get; set; }
        public bool IsAutoIncrementEmployeeNo { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
