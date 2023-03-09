using CommonServiceLocator;
using EntityFrameworkCore.Triggers;
using UniversityManagementSystemPortal.IdentityServices;

namespace UniversityManagementSystemPortal.TrackableBaseEntity

{
    public abstract class TrackableBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        static TrackableBaseEntity()
        {
            Triggers<TrackableBaseEntity>.Inserting += entry =>
            {
                entry.Entity.Id = Guid.NewGuid();
                entry.Entity.CreatedAt = entry.Entity.UpdatedAt = DateTime.UtcNow;
              
            };

            Triggers<TrackableBaseEntity>.Updating += entry =>
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
               
            };
        }

        
    }
}
