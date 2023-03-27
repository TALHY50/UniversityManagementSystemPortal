namespace UniversityManagementSystemPortal.ModelDto.InstituteAdmin
{
    public class InstituteAdminDTO
    {

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? InstituteName { get; set; }
        public Guid? InstituteId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
