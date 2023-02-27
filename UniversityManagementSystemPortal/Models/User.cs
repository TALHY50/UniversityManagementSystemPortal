using System;
using System.Collections.Generic;

namespace UniversityManagementsystem.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? MobileNo { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public int? Gender { get; set; }

    public int? BloodGroup { get; set; }

    public string Email { get; set; } = null!;

    public string Username { get; set; } = null!;

    public bool EmailConfirmed { get; set; }

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? LastLoggedIn { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();

    public virtual ICollection<InstituteAdmin> InstituteAdmins { get; } = new List<InstituteAdmin>();

    public virtual ICollection<Student> Students { get; } = new List<Student>();

    public virtual ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
}
