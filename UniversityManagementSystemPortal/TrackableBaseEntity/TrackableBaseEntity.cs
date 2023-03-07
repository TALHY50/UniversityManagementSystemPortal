using CommonServiceLocator;
using EntityFrameworkCore.Triggers;
using UniversityManagementSystemPortal.IdentityServices;

namespace UniversityManagementSystemPortal.TrackableBaseEntity

{
    public abstract class TrackableBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }

        static TrackableBaseEntity()
        {
            Triggers<TrackableBaseEntity>.Inserting += entry =>
            {
                var userId = ServiceLocator.Current.GetInstance<IIdentityServices>().GetUserId();
                entry.Entity.Id = Guid.NewGuid();
                entry.Entity.CreatedAt = entry.Entity.UpdatedAt = DateTime.UtcNow;
                entry.Entity.CreatedBy = userId;
                entry.Entity.UpdatedBy = userId;
            };
            Triggers<TrackableBaseEntity>.Updating += entry =>
            {
                var userId = ServiceLocator.Current.GetInstance<IIdentityServices>().GetUserId();
                entry.Entity.UpdatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedBy = userId;
            };
        }
    }


}
