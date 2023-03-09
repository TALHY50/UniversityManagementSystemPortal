using System.ComponentModel.DataAnnotations;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.ModelDto.Department;
using UniversityManagementSystemPortal.ModelDto.StudentProgram;

namespace UniversityManagementSystemPortal
{
    public class ProgramReadDto
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Code { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        [StringLength(100)]
        public string? SectionName { get; set; }

        [Display(Name = "GradeType")]
        [EnumDataType(typeof(GradeType))]
        public GradeType GradingType { get; set; }
        public int? DepartmentCode { get; set; }
        public string? DepartmentName { get; set; }

        public string? InstituteName { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }


    }
}