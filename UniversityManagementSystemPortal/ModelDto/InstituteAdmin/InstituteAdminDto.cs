namespace UniversityManagementSystemPortal.ModelDto.InstituteAdmin
{
    public class InstituteAdminDTO
    {

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? InstituteName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool IsSuperAdmin { get; set; }
        public Guid? InstituteId { get; set; }
    }
}
