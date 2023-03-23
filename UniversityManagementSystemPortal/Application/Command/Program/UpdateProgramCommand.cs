using MediatR;
using System.ComponentModel.DataAnnotations;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.ModelDto.Program;

namespace UniversityManagementSystemPortal.Application.Command.Program
{
    public class UpdateProgramCommand : IRequest
    {
        public Guid Id { get; set; }
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
        public Guid UpdatedBy { get; set; }

    }
}
