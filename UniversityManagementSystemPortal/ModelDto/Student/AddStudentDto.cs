using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal
{
    public class AddStudentDto
    {
        public Guid? UserId { get; set; }
        public string AdmissionNo { get; set; }
        //public StudentCategory? Category { get; set; }
        public string Address { get; set; }
        public Guid InstituteId { get; set; }
       
       

    }
}
