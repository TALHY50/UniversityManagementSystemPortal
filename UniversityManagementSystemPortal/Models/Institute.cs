using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementsystem.Models;

public partial class Institute
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    [JsonRequired]
    [JsonPropertyName("Type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public InstituteType Type { get; set; }

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Website { get; set; }

    public string? Address { get; set; }

    public bool IsAutoIncrementAdmissionNo { get; set; }

    public bool IsAutoIncrementEmployeeNo { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual ICollection<Category> Categories { get; } = new List<Category>();

    public virtual ICollection<Department> Departments { get; } = new List<Department>();

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();

    public virtual ICollection<InstituteAdmin> InstituteAdmins { get; } = new List<InstituteAdmin>();

    public virtual ICollection<Student> Students { get; } = new List<Student>();
}
