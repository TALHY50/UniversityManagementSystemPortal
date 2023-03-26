using UniversityManagementSystemPortal.ModelDto.Institute;
using UniversityManagementSystemPortal.Models.ModelDto.Institute;
using UniversityManagementSystemPortal.Models.ModelDto.Program;

namespace UniversityManagementSystemPortal.ModelDto.Department
{
    public class DepartmentReadDto
    {
         public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsAcademics { get; set; }
        public bool IsAdministrative { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public InstituteReadDto Institute { get; set; }
        //public ICollection<EmployeeReadDto> Employees { get; set; }
        public ICollection<ProgramReadDto> Programs { get; set; }
    }

}
