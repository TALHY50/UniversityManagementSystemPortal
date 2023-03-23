using MediatR;
using UniversityManagementSystemPortal.ModelDto.Category;

namespace UniversityManagementSystemPortal.Application.Command.Category
{
    public class CreateCategoryCommand : IRequest<CategoryDto>
    {
        public CategoryCreateDto CreateCategoryDto { get; set; }
    }
}
