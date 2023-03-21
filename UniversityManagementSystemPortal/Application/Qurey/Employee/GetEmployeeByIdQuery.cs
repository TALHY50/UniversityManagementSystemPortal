using MediatR;
using UniversityManagementSystemPortal.ModelDto.Employee;

namespace UniversityManagementSystemPortal.Application.Qurey.Employee
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeDto>
    {
        public Guid Id { get; set; }
    }
}
