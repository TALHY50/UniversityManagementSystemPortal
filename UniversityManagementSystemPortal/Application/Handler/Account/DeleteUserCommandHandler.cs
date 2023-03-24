using MediatR;
using UniversityManagementSystemPortal.Application.Command.Account;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfce;

namespace UniversityManagementSystemPortal.Application.Handler.Account
{
    public class DeleteUserByIdHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUserInterface _userRepository;
        private readonly ILogger<DeleteUserByIdHandler> _logger;

        public DeleteUserByIdHandler(IUserInterface userRepository, ILogger<DeleteUserByIdHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userToDelete = await _userRepository.GetByIdAsync(request.UserId);

            if (userToDelete == null)
            {
                throw new AppException($"User with ID {request.UserId} not found.");
            }

            await _userRepository.DeleteAsync(userToDelete);

            _logger.LogInformation("User with ID {UserId} deleted successfully.", request.UserId);

            return Unit.Value;
        }
    }
}
