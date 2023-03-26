using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.StudentProgram;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.StudentProgram;
using UniversityManagementSystemPortal.Models.ModelDto.StudentProgram;

namespace UniversityManagementSystemPortal.Application.Handler.StudentProgram
{
    public class GetAllStudentProgramsQueryHandler : IRequestHandler<GetAllStudentProgramsQuery, List<StudentProgramDto>>
    {
        private readonly IStudentProgramRepository _studentProgramRepository;
        private readonly IMapper _mapper;

        public GetAllStudentProgramsQueryHandler(IStudentProgramRepository studentProgramRepository, IMapper mapper)
        {
            _studentProgramRepository = studentProgramRepository;
            _mapper = mapper;
        }

        public async Task<List<StudentProgramDto>> Handle(GetAllStudentProgramsQuery request, CancellationToken cancellationToken)
        {
            var studentPrograms = await _studentProgramRepository.GetAllStudentProgramsAsync();
            var studentProgramDtos = _mapper.Map<IEnumerable<StudentProgramDto>>(studentPrograms);
            return studentProgramDtos.ToList();
        }
    }
}
