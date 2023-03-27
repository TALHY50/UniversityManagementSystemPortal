using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Department;
using UniversityManagementSystemPortal.Helpers.FilterandSorting;
using UniversityManagementSystemPortal.Helpers.Paging;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Models.ModelDto.Department;
using UniversityManagementSystemPortal.Repository;

namespace UniversityManagementSystemPortal
{
    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, PaginatedList<DepartmentDto>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public GetAllDepartmentsQueryHandler(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<DepartmentDto>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var paginatedViewModel = request.paginatedViewModel;
            var departments =  _departmentRepository.GetAllDepartmentsAsync();
            var propertyNames = new[] { paginatedViewModel.columnName }; // assuming only one property for filtering
            var filteredDepartments = Filtering.Filter(departments, paginatedViewModel.search, propertyNames);
            var sortedDepartments = Sorting<Department>.Sort(paginatedViewModel.SortBy, paginatedViewModel.columnName, filteredDepartments);
            var paginatedDepartments = PaginationHelper.Create(sortedDepartments, paginatedViewModel);
            var departmentDto = _mapper.Map<PaginatedList<DepartmentDto>>(paginatedDepartments);
            return await Task.FromResult(departmentDto);
        }
    }

}
