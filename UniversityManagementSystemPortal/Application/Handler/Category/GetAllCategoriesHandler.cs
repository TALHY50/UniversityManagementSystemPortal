using AutoMapper;
using Azure.Core;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Category;
using UniversityManagementSystemPortal.Helpers.FilterandSorting;
using UniversityManagementSystemPortal.Helpers.Paging;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Models.ModelDto.Department;
using UniversityManagementSystemPortal.Repository;

namespace UniversityManagementSystemPortal
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, PaginatedList<CategoryDto>>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public GetAllCategoriesHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<CategoryDto>> Handle(GetAllCategoriesQuery query, CancellationToken cancellationToken)
        {
            var paginatedViewModel = query.paginatedViewModel;
            var categories = await _repository.GetAllAsync();
            var propertyNames = new[] { paginatedViewModel.columnName };
            var filteredCategories = Filtering.Filter(categories, paginatedViewModel.search, propertyNames);
            var sortedCategories = Sorting<Category>.Sort(paginatedViewModel.SortBy, paginatedViewModel.columnName, filteredCategories);
            var paginatedCategories = PaginationHelper.Create(sortedCategories, paginatedViewModel);
            var categoryDto = _mapper.Map<PaginatedList<CategoryDto>>(paginatedCategories);
            return await Task.FromResult(categoryDto);
        }
    }

}
