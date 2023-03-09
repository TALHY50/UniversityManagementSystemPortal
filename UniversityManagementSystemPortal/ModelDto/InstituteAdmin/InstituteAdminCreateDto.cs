namespace UniversityManagementSystemPortal.ModelDto.InstituteAdmin
{
    public class InstituteAdminCreateDto
    {
        public Guid UserId { get; set; }
        public Guid? InstituteId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}
