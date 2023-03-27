using MediatR;
using UniversityManagementSystemPortal.Helpers.Paging;
using UniversityManagementSystemPortal.Models.ModelDto.Department;

namespace UniversityManagementSystemPortal.Application.Qurey.Department
{
    public class GetAllDepartmentsQuery : IRequest<PaginatedList<DepartmentDto>>
    {
        public PaginatedViewModel? paginatedViewModel { get; set; }
    }
}
