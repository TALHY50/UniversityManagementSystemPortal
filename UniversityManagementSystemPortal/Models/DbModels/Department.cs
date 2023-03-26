using UniversityManagementSystemPortal.Models.TrackableBaseEntity;

namespace UniversityManagementSystemPortal
{
    public partial class Department : TrackableBaseEntity
    {
        public Guid Id { get; set; }

        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public Guid InstituteId { get; set; }

        public bool IsActive { get; set; }

        public bool IsAcademics { get; set; }

        public bool IsAdministrative { get; set; }


        public Guid? CreatedBy { get; set; }

        public Guid? UpdatedBy { get; set; }

        public virtual ICollection<Employee> Employees { get; } = new List<Employee>();

        public virtual Institute Institute { get; set; } = null!;

        public virtual ICollection<Program> Programs { get; } = new List<Program>();
    }
}