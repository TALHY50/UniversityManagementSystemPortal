using System.ComponentModel.DataAnnotations;
using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal.ModelDto.Institute
{
    public class InstituteCreateDto
    {
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
