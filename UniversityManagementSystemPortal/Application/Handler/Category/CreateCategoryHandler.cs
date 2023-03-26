using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Command.Category;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CreateCategoryHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(command.CreateCategoryDto);
            //category.cre = DateTime.UtcNow;
            category.IsActive = true;
            category = await _repository.AddAsync(category);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }
    }

}
