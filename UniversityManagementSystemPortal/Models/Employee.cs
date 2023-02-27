using System;
using System.Collections.Generic;

namespace UniversityManagementsystem.Models;

public partial class Employee
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public string EmployeeNo { get; set; } = null!;

    public int? EmployeeType { get; set; }

    public string? Address { get; set; }

    public DateTime? JoiningDate { get; set; }

    public Guid InstituteId { get; set; }

    public string? ProfilePath { get; set; }

    public Guid? DepartmentId { get; set; }

    public Guid? PositionId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Institute Institute { get; set; } = null!;

    public virtual Position? Position { get; set; }

    public virtual User? User { get; set; }
}
