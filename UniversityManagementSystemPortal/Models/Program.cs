using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.TrackableBaseEntity;

namespace UniversityManagementsystem.Models;

public partial class Program : TrackableBaseEntity
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

    public Guid? DepartmentId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<StudentProgram> StudentPrograms { get; } = new List<StudentProgram>();
}
