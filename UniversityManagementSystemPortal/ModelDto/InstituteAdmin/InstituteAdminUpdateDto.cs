namespace UniversityManagementSystemPortal.ModelDto.InstituteAdmin
{
    public class InstituteAdminUpdateDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? InstituteId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}
