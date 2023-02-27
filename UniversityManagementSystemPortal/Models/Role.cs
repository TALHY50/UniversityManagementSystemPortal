using System;
using System.Collections.Generic;

namespace UniversityManagementsystem.Models;

public partial class Role
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public int? RoleType { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}
