using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using UniversityManagementSystemPortal.Application.Qurey.Student;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Application.Handler.Student
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQurey, StudentDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetStudentByIdQueryHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<StudentDto> Handle(GetStudentByIdQurey request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetById(request.Id);
            if (student == null)
            {
                throw new AppException(nameof(Student), request.Id);
            }
            var studentDto = _mapper.Map<StudentDto>(student);
            return studentDto;
        }
    }

}
