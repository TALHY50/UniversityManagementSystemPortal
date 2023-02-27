﻿using System;
using System.Collections.Generic;

namespace UniversityManagementsystem.Models;

public partial class Program
{
    public Guid Id { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string SectionName { get; set; } = null!;

    public int GradingType { get; set; }

    public Guid? DepartmentId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<StudentProgram> StudentPrograms { get; } = new List<StudentProgram>();
}