using MediatR;
using UniversityManagementSystemPortal.ModelDto.Department;

namespace UniversityManagementSystemPortal.Application.Command.Department
{
    public class CreateDepartmentCommand : IRequest<DepartmentCreateDto>
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public Guid InstituteId { get; set; }
        public bool IsActive { get; set; }
        public bool IsAcademics { get; set; }
        public bool IsAdministrative { get; set; }
    }
}
