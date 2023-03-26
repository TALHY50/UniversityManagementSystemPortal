namespace UniversityManagementSystemPortal.Models.ModelDto.StudentProgram
{
    public class StudentProgramCreateDto
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public Guid ProgramId { get; set; }

        public string RoleNo { get; set; } = null!;

    }
}
