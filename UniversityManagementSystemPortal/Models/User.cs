using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.Models.TrackableBaseEntity;

namespace UniversityManagementsystem.Models;

public partial class User : TrackableBaseEntity
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
    public string FirstName { get; set; }

    public string? MiddleName { get; set; }

    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
    public string? LastName { get; set; }

    [RegularExpression(@"^\d{11}$", ErrorMessage = "Mobile number must be 11 digits")]
    public string? MobileNo { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? DateOfBirth { get; set; }
    [Display(Name = "Gender")]
    [EnumDataType(typeof(Gender))]
    public Gender Gender { get; set; }

    [Display(Name = "BloodGroup")]
    [EnumDataType(typeof(BloodGroup))]
    public BloodGroup BloodGroup { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Username is required")]
    [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters")]
    public string Username { get; set; }

    public bool EmailConfirmed { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least 8 characters, one uppercase letter, one lowercase letter, one number, and one special character")]
    public string Password { get; set; }

    public bool IsActive { get; set; }

    public DateTime? LastLoggedIn { get; set; }


    public Guid? CreatedBy { get; set; }


    public Guid? UpdatedBy { get; set; }

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();

    public virtual ICollection<InstituteAdmin> InstituteAdmins { get; } = new List<InstituteAdmin>();

    public virtual ICollection<Student> Students { get; } = new List<Student>();

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();


}
