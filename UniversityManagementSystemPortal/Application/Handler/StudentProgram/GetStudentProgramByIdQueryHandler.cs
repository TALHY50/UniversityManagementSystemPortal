using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.StudentProgram;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.StudentProgram;
using UniversityManagementSystemPortal.Models.ModelDto.StudentProgram;

namespace UniversityManagementSystemPortal.Application.Handler.StudentProgram
{
    public class GetStudentProgramByIdQueryHandler : IRequestHandler<GetStudentProgramByIdQuery, StudentProgramDto>
    {
        private readonly IStudentProgramRepository _studentProgramRepository;
        private readonly IMapper _mapper;

        public GetStudentProgramByIdQueryHandler(IStudentProgramRepository studentProgramRepository, IMapper mapper)
        {
            _studentProgramRepository = studentProgramRepository;
            _mapper = mapper;
        }

        public async Task<StudentProgramDto> Handle(GetStudentProgramByIdQuery request, CancellationToken cancellationToken)
        {
            var studentProgram = await _studentProgramRepository.GetStudentProgramByIdAsync(request.Id);
            if (studentProgram == null)
            {
                return null;
            }
            var studentProgramDto = _mapper.Map<StudentProgramDto>(studentProgram);
            return studentProgramDto;
        }
    }
}