using MediatR;

namespace UniversityManagementSystemPortal.Application.Qurey.Category
{
    public class GetCategoryByIdQuery : IRequest<CategoryDto>
    {
        public Guid Id { get; set; }
    }
}
