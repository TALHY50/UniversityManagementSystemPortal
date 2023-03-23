using AutoMapper;
using MediatR;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Application.Command.StudentProgram;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.StudentProgram;

namespace UniversityManagementSystemPortal
{
    public class AddStudentProgramCommandHandler : IRequestHandler<AddStudentProgramCommand, StudentProgramDto>
    {
        private readonly IStudentProgramRepository _studentProgramRepository;
        private readonly IMapper _mapper;

        public AddStudentProgramCommandHandler(IStudentProgramRepository studentProgramRepository, IMapper mapper)
        {
            _studentProgramRepository = studentProgramRepository;
            _mapper = mapper;
        }

        public async Task<StudentProgramDto> Handle(AddStudentProgramCommand request, CancellationToken cancellationToken)
        {
            var studentProgram = _mapper.Map<StudentProgram>(request.StudentProgramCreateDto);
            await _studentProgramRepository.AddStudentProgramAsync(studentProgram);
            var studentProgramDto = _mapper.Map<StudentProgramDto>(studentProgram);
            return studentProgramDto;
        }
    }
}
