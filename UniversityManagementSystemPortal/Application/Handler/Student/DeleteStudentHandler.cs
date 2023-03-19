using MediatR;
using UniversityManagementSystemPortal.Application.Command.Student;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.PictureManager;

namespace UniversityManagementSystemPortal.Application.Handler.Student
{
    public class DeleteStudentHandler : IRequestHandler<DeleteStudentCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IPictureManager _pictureManager;
        private readonly ILogger<DeleteStudentHandler> _logger;

        public DeleteStudentHandler(IStudentRepository studentRepository, IPictureManager pictureManager, ILogger<DeleteStudentHandler> logger)
        {
            _studentRepository = studentRepository;
            _pictureManager = pictureManager;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingStudent = await _studentRepository.GetById(request.Id);
                if (existingStudent == null)
                {
                    _logger.LogError("Student with Id {Id} not found.", request.Id);
                    return Unit.Value;
                }

                // Delete profile picture
                if (!string.IsNullOrEmpty(existingStudent.ProfilePath))
                {
                    _pictureManager.Delete(existingStudent.ProfilePath);
                }

                await _studentRepository.Delete(request.Id);
                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting student with Id {Id}", request.Id);
                throw;
            }
        }
    }

}
