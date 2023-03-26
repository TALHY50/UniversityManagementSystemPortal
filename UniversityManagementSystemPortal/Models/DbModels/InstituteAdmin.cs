using System;
using System.Collections.Generic;
using UniversityManagementSystemPortal.Models.TrackableBaseEntity;

namespace UniversityManagementSystemPortal
{
    public partial class InstituteAdmin : TrackableBaseEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid InstituteId { get; set; }

        public Guid? CreatedBy { get; set; }

        public Guid? UpdatedBy { get; set; }

        public virtual Institute Institute { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}