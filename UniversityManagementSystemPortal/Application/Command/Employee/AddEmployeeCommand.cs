using MediatR;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.ModelDto.Employee;

namespace UniversityManagementSystemPortal.Application.Command.Employee
{
    public record CreateEmployeeCommand(CreateEmployeeDto createEmployeeDto): IRequest<CreateEmployeeDto>;
   
}
