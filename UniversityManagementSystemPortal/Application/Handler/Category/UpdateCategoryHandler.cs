using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Command.Category;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Application.Handler.Category
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public UpdateCategoryHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(command.Id);

            if (category == null)
            {
                throw new AppException(nameof(Category), command.Id);
            }

            _mapper.Map(command.UpdateCategoryDto, category);
            category.UpdatedAt = DateTime.UtcNow;
            category = await _repository.UpdateAsync(category);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }
    }

}
