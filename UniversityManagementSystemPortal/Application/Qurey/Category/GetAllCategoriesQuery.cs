using MediatR;
using UniversityManagementSystemPortal.Helpers.Paging;

namespace UniversityManagementSystemPortal.Application.Qurey.Category
{
    public class GetAllCategoriesQuery : IRequest<PaginatedList<CategoryDto>>
    {
        public PaginatedViewModel? paginatedViewModel { get; set; }
    }
}
