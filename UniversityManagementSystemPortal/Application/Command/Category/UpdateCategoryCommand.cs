using MediatR;
using UniversityManagementSystemPortal.ModelDto.Category;

namespace UniversityManagementSystemPortal.Application.Command.Category
{
    public class UpdateCategoryCommand : IRequest<CategoryDto>
    {
        public Guid Id { get; set; }
        public CategoryUpdateDto UpdateCategoryDto { get; set; }
    }

}
