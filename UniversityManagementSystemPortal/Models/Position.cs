﻿using System;
using System.Collections.Generic;
using UniversityManagementSystemPortal.Models.TrackableBaseEntity;

namespace UniversityManagementsystem.Models;

public partial class Position : TrackableBaseEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid? CategoryId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
