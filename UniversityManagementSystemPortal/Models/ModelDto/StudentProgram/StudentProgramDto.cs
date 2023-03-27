namespace UniversityManagementSystemPortal.ModelDto.StudentProgram
{
    public class StudentProgramDto
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public Guid ProgramId { get; set; }

        public string RoleNo { get; set; } = null!;

        public string? Student_Name { get; set; }
        public string? Program_Name { get; set; }
        public DateTime? CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }

    }
}
