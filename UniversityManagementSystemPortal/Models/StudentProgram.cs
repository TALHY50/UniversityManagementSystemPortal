using System;
using System.Collections.Generic;
using UniversityManagementSystemPortal.TrackableBaseEntity;

namespace UniversityManagementsystem.Models;

public partial class StudentProgram: TrackableBaseEntity
{
    public Guid Id { get; set; }

    public Guid StudentId { get; set; }

    public Guid ProgramId { get; set; }

    public string RoleNo { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual Program Program { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
