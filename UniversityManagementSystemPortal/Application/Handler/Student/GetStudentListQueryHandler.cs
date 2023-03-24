using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Student;
using UniversityManagementSystemPortal.Helpers.Paging;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Application.Handler.Student
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
            var paginatedViewModel = request.PaginatedViewModel;
            var students = await _studentRepository.Get(paginatedViewModel);

            var studentDtos = _mapper.Map<List<StudentDto>>(students);

            return PaginatedList<StudentDto>.Create(studentDtos, paginatedViewModel);
        }
    }

}
