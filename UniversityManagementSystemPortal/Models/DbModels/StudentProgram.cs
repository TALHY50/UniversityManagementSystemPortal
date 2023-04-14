﻿using UniversityManagementSystemPortal.Models.TrackableBaseEntity;

namespace UniversityManagementSystemPortal
{
    public partial class StudentProgram : TrackableBaseEntity
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public Guid? ProgramId { get; set; }

        public string RoleNo { get; set; } = null!;

        public bool? IsActive { get; set; }

        public Guid? CreatedBy { get; set; }

        public Guid? UpdatedBy { get; set; }

        public virtual Program Program { get; set; } = null!;

        public virtual Student Student { get; set; } = null!;
    }
}