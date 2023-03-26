using MediatR;
using System.ComponentModel.DataAnnotations;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.Models.ModelDto.Program;

namespace UniversityManagementSystemPortal.Application.Command.Program
{
    public class CreateProgramCommand :  IRequest<ProgramReadDto>
    {
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
        public Guid DepartmentId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }
    }
}
