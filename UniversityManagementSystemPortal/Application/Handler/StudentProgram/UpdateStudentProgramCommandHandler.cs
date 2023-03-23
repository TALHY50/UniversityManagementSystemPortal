using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Command.StudentProgram;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Application.Handler.StudentProgram
{
    public class UpdateStudentProgramCommandHandler : IRequestHandler<UpdateStudentProgramCommand>
    {
        private readonly IStudentProgramRepository _studentProgramRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateStudentProgramCommandHandler> _logger;

        public UpdateStudentProgramCommandHandler(IStudentProgramRepository studentProgramRepository, IMapper mapper, ILogger<UpdateStudentProgramCommandHandler> logger)
        {
            _studentProgramRepository = studentProgramRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateStudentProgramCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating student program with ID {Id}", request.Id);

            var studentProgram = await _studentProgramRepository.GetStudentProgramByIdAsync(request.Id);
            if (studentProgram == null)
            {
                _logger.LogWarning("Student program with ID {Id} not found", request.Id);
                throw new AppException(nameof(StudentProgram), request.Id);
            }

            _mapper.Map(request.StudentProgramUpdateDto, studentProgram);
            await _studentProgramRepository.UpdateStudentProgramAsync(studentProgram);

            _logger.LogInformation("Student program with ID {Id} updated successfully", request.Id);

            return Unit.Value;
        }
    }

}
