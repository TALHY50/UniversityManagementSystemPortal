using MediatR;

namespace UniversityManagementSystemPortal.Application.Qurey.Category
{
    public class GetAllCategoriesQuery : IRequest<List<CategoryDto>>
    {
    }
}
