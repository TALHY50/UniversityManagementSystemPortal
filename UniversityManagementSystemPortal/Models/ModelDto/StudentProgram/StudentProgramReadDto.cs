using UniversityManagementSystemPortal.ModelDto.Program;

namespace UniversityManagementSystemPortal.ModelDto.StudentProgram
{
    public class StudentProgramReadDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string RoleNo { get; set; }
        public bool? IsActive { get; set; }
       
    }
}