using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Student;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Application.Handler.Student
{
    public class GetStudentByAdmissionNoQueryHandler : IRequestHandler<GetStudentByAdmissionNoQuery, StudentDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetStudentByAdmissionNoQueryHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<StudentDto> Handle(GetStudentByAdmissionNoQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByAdmissionNo(request.AdmissionNo);
            if (student == null)
            {
                throw new AppException($"Student with admission number {request.AdmissionNo} not found.");
            }

            return _mapper.Map<StudentDto>(student);
        }
    }
}
