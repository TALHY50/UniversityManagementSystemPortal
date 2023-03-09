using UniversityManagementSystemPortal.ModelDto.Program;

namespace UniversityManagementSystemPortal.ModelDto.StudentProgram
{
    public class StudentProgramReadDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string RoleNo { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public StudentReadDto Student { get; set; }
        public ProgramReadDto Program { get; set; }
    }
}