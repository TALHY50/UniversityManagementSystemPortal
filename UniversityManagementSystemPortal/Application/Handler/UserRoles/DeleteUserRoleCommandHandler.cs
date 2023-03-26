using MediatR;
using UniversityManagementSystemPortal.Application.Command.UserRoles;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Application.Handler.UserRoles
{
    public class DeleteUserRoleCommandHandler : IRequestHandler<DeleteUserRoleCommand>
    {
        private readonly IUserRoleRepository _repository;
        private readonly ILogger<DeleteUserRoleCommandHandler> _logger;

        public DeleteUserRoleCommandHandler(IUserRoleRepository repository, ILogger<DeleteUserRoleCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
        {
            var userRole = await _repository.GetByIdAsync(request.Id);

            if (userRole == null)
            {
                _logger.LogWarning("User role with id {Id} not found.", request.Id);
                throw new AppException(nameof(UserRole), request.Id);
            }

            await _repository.DeleteAsync(userRole);

            _logger.LogInformation("User role with id {Id} deleted successfully.", request.Id);

            return Unit.Value;
        }
    }
}
