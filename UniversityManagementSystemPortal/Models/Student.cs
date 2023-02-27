using System;
using System.Collections.Generic;

namespace UniversityManagementsystem.Models;

public partial class Student
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public string AdmissionNo { get; set; } = null!;

    public int? Category { get; set; }

    public string? Address { get; set; }

    public Guid InstituteId { get; set; }

    public string? ProfilePath { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual Institute Institute { get; set; } = null!;

    public virtual ICollection<StudentProgram> StudentPrograms { get; } = new List<StudentProgram>();

    public virtual User? User { get; set; }
}
