using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal.Models.ModelDto.Institute
{
    public class InstituteReadDto
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
    }
}
