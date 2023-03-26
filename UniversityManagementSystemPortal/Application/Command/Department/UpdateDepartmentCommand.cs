using MediatR;
using UniversityManagementSystemPortal.Models.ModelDto.Department;

namespace UniversityManagementSystemPortal.Application.Command.Department
{
    public class UpdateDepartmentCommand : IRequest<DepartmentUpdateDto>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid InstituteId { get; set; }
        public bool IsActive { get; set; }
        public bool IsAcademics { get; set; }
        public bool IsAdministrative { get; set; }
    }

}
