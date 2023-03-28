﻿
using System.ComponentModel.DataAnnotations;
using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string? CategoryName { get; set; }
        public string? Prefix { get; set; }
        public bool IsActive { get; set; }
        public bool IsStaff { get; set; }
        public bool IsFaculty { get; set; }
        public string InstituteName { get; set; } = null!;

        [Display(Name = "InstituteType")]
        [EnumDataType(typeof(InstituteType))]
        public InstituteType Type { get; set; }

        [Required]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid phone number.")]
        public string Phone { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = null!;

        [Url(ErrorMessage = "Invalid website URL.")]
        public string? Website { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }
        public bool IsAutoIncrementAdmissionNo { get; set; }
        public bool IsAutoIncrementEmployeeNo { get; set; }
        public DateTime? CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }

    }
}
