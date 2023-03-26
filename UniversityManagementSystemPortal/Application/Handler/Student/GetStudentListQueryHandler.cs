using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Student;
using UniversityManagementSystemPortal.Helpers.FilterandSorting;
using UniversityManagementSystemPortal.Helpers.Paging;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Models.ModelDto.Student;

namespace UniversityManagementSystemPortal
{
    public class GetStudentListQueryHandler : IRequestHandler<GetStudentListQuery, PaginatedList<StudentDto>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetStudentListQueryHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }
        public async Task<PaginatedList<StudentDto>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var paginatedViewModel = request.paginatedViewModel;
            var students = _studentRepository.Get();
            var propertyNames = new[] { paginatedViewModel.columnName }; // assuming only one property for filtering
            var filteredStudents = Filtering.Filter(students, paginatedViewModel.search, propertyNames);
            var sortedStudents = Sorting<Student>.Sort(paginatedViewModel.SortBy, paginatedViewModel.columnName, filteredStudents);
            var paginatedStudents = PaginationHelper.Create(sortedStudents, paginatedViewModel);
            var studentDtos = _mapper.Map<PaginatedList<StudentDto>>(paginatedStudents);
            return await Task.FromResult(studentDtos);
        }
    }

}
