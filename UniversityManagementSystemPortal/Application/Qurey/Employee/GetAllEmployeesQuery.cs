using MediatR;
using UniversityManagementSystemPortal.Helpers.Paging;
using UniversityManagementSystemPortal.ModelDto.Employee;

namespace UniversityManagementSystemPortal.Application.Qurey.Employee
{
    public class GetAllEmployeesQuery : IRequest<PaginatedList<EmployeeDto>>
    {
        public PaginatedViewModel? paginatedViewModel { get; set; }
    }
}
