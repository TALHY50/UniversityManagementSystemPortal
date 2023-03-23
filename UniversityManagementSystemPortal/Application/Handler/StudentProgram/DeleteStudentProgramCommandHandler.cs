using MediatR;
using UniversityManagementSystemPortal.Application.Command.StudentProgram;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Application.Handler.StudentProgram
{
    public class DeleteStudentProgramCommandHandler : IRequestHandler<DeleteStudentProgramCommand>
    {
        private readonly IStudentProgramRepository _studentProgramRepository;
        private readonly ILogger<DeleteStudentProgramCommandHandler> _logger;

        public DeleteStudentProgramCommandHandler(IStudentProgramRepository studentProgramRepository, ILogger<DeleteStudentProgramCommandHandler> logger)
        {
            _studentProgramRepository = studentProgramRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteStudentProgramCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting student program with ID {Id}", request.Id);

            var studentProgram = await _studentProgramRepository.GetStudentProgramByIdAsync(request.Id);
            if (studentProgram == null)
            {
                _logger.LogWarning("Student program with ID {Id} not found", request.Id);
                throw new AppException(nameof(StudentProgram), request.Id);
            }

            await _studentProgramRepository.DeleteStudentProgramAsync(request.Id);

            _logger.LogInformation("Student program with ID {Id} deleted successfully", request.Id);

            return Unit.Value;
        }
    }
}
