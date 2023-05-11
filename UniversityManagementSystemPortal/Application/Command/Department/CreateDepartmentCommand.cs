using MediatR;
using UniversityManagementSystemPortal.ModelDto.Department;
using UniversityManagementSystemPortal.Models.ModelDto.Department;

namespace UniversityManagementSystemPortal.Application.Command.Department
{
    public record CreateDepartmentCommand(DepartmentCreateDto departmentCreateDto) : IRequest<DepartmentCreateDto>;
   
}
