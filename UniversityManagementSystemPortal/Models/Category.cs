using System;
using System.Collections.Generic;
using UniversityManagementSystemPortal.TrackableBaseEntity;

namespace UniversityManagementsystem.Models;

public partial class Category
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Prefix { get; set; }

    public Guid InstituteId { get; set; }

    public bool IsActive { get; set; }

    public bool IsStaff { get; set; }

    public bool IsFaculty { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual Institute Institute { get; set; } = null!;

    public virtual ICollection<Position> Positions { get; } = new List<Position>();
}
