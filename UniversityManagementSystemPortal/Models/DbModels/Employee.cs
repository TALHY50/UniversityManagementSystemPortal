﻿using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.Models.TrackableBaseEntity;

namespace UniversityManagementSystemPortal
{
    public partial class Employee : TrackableBaseEntity
    {
        public Guid Id { get; set; }

        public Guid? UserId { get; set; }

        public string EmployeeNo { get; set; } = null!;

        public EmployeeType EmployeeType { get; set; }

        public string? Address { get; set; }

        public DateTime? JoiningDate { get; set; }

        public Guid InstituteId { get; set; }

        public string? ProfilePath { get; set; }

        public Guid? DepartmentId { get; set; }

        public Guid? PositionId { get; set; }

        public bool IsActive { get; set; }

        public Guid? CreatedBy { get; set; }


        public Guid? UpdatedBy { get; set; }

        public virtual Department? Department { get; set; }

        public virtual Institute Institute { get; set; } = null!;

        public virtual Position? Position { get; set; }

        public virtual User? User { get; set; }
    }
}