using MediatR;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.ModelDto.Employee;

namespace UniversityManagementSystemPortal.Application.Command.Employee
{
    public class CreateEmployeeCommand : IRequest<CreateEmployeeDto>
    {
        public string EmployeeNo { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public string? Address { get; set; }
        public DateTime? JoiningDate { get; set; }
        public Guid InstituteId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? PositionId { get; set; }
        public Guid? UserId { get; set; }
        public IFormFile Picture { get; set; }
    }
}
