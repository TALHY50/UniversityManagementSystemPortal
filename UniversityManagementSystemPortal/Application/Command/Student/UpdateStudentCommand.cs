using MediatR;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.Models.ModelDto.Student;

namespace UniversityManagementSystemPortal.Application.Command.Student
{
    public class UpdateStudentCommand : IRequest<UpdateStudentDto>
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string AdmissionNo { get; set; }
        public StudentCategory? Category { get; set; }
        public string Address { get; set; }
        public Guid InstituteId { get; set; }
        public IFormFile Picture { get; set; }
        public bool IsActive { get; set; }
    }
}
