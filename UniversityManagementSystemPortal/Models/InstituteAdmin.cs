using System;
using System.Collections.Generic;
using UniversityManagementSystemPortal.TrackableBaseEntity;

namespace UniversityManagementsystem.Models;

public partial class InstituteAdmin 
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid InstituteId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual Institute Institute { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
