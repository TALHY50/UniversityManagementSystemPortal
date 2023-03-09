using UniversityManagementSystemPortal.ModelDto.Department;
using UniversityManagementSystemPortal.ModelDto.StudentProgram;

namespace UniversityManagementSystemPortal
{
    public class ProgramReadDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string SectionName { get; set; }
        public int GradingType { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DepartmentReadDto Department { get; set; }
        public ICollection<StudentProgramReadDto> StudentPrograms
        {
            get; set;
        }
    }
}