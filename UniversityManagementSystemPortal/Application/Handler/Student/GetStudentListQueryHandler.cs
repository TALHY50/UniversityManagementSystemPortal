using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Student;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Application.Handler.Student
{
    public class GetStudentListQueryHandler : IRequestHandler<GetStudentListQurey, List<StudentDto>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetStudentListQueryHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<List<StudentDto>> Handle(GetStudentListQurey request, CancellationToken cancellationToken)
        {
            var students = await _studentRepository.Get();
            var studentDtos = _mapper.Map<List<StudentDto>>(students);
            return studentDtos;
        }

    }
}
