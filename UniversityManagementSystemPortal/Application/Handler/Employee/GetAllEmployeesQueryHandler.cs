using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Employee;
using UniversityManagementSystemPortal.Helpers.FilterandSorting;
using UniversityManagementSystemPortal.Helpers.Paging;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Employee;

namespace UniversityManagementSystemPortal
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, PaginatedList<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var paginatedViewModel = request.paginatedViewModel;
            var employees = _employeeRepository.GetAllAsync();
            var propertyNames = new[] { paginatedViewModel?.columnName }; // assuming only one property for filtering
            var filteredemployees = Filtering.Filter(employees, paginatedViewModel.search, propertyNames);
            var sortedemployees = Sorting<Employee>.Sort(paginatedViewModel.SortBy, paginatedViewModel.columnName, filteredemployees);
            var paginatedemployees = PaginationHelper.Create(sortedemployees, paginatedViewModel);
            var studentDtos = _mapper.Map<PaginatedList<EmployeeDto>>(paginatedemployees);
            return await Task.FromResult(studentDtos);
        }
    }
}
