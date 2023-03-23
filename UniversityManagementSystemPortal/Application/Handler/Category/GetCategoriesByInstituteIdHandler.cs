using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Category;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Application.Handler.Category
{
    public class GetCategoriesByInstituteIdHandler : IRequestHandler<GetCategoriesByInstituteIdQuery, List<CategoryDto>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public GetCategoriesByInstituteIdHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> Handle(GetCategoriesByInstituteIdQuery query, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetByInstituteIdAsync(query.InstituteId);
            var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return categoryDtos.ToList();
        }
    }
}
