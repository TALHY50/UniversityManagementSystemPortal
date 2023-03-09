using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementsystem.Models;

public partial class Role 
{
    public Guid Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Display(Name = "RoleType")]
    [EnumDataType(typeof(RoleType))]
    public RoleType? RoleType { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}
