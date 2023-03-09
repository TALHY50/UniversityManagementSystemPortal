using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.ModelDto.InstituteDto;
using UniversityManagementSystemPortal.ModelDto.Student;

namespace UniversityManagementSystemPortal
{
    public class StudentDto
    {
        public string AdmissionNo { get; set; }
        //public StudentCategory? Category { get; set; }
        public string? Address { get; set; }
        public UserReadModel? user { get; set; }
        public string? ProfilePath { get; set; }
        public bool IsActive { get; set; }
        public InstituteDto Institute { get; set; }
        public DepartmentReadModel? Department { get; set; }
        public ProgramReadModel? Program { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        


    }
}
