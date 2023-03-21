using MediatR;
using UniversityManagementSystemPortal.ModelDto.Employee;

namespace UniversityManagementSystemPortal.Application.Qurey.Employee
{
    public class GetAllEmployeesQuery : IRequest<IEnumerable<EmployeeDto>>
    {
    }
}
