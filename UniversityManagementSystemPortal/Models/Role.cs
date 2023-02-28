using System;
using System.Collections.Generic;
using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementsystem.Models;

public partial class Role
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public RoleType RoleType { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}
