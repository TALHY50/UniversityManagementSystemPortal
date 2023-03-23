using MediatR;
using UniversityManagementSystemPortal.Application.Command.Position;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Application.Handler.Postion
{
    public class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand, bool>
    {
        private readonly IIdentityServices _identityServices;
        private readonly IPositionRepository _positionRepository;
        private readonly IInstituteAdminRepository _repository;
        private readonly ILogger<DeletePositionCommandHandler> _logger;

        public DeletePositionCommandHandler(IIdentityServices identityServices,IInstituteAdminRepository repository,IPositionRepository positionRepository, ILogger<DeletePositionCommandHandler> logger)
        {
            _positionRepository = positionRepository;
            _repository = repository;
            _logger = logger;
            _identityServices = identityServices;
        }

        public async Task<bool> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
        {
            var activeUserId = _identityServices.GetUserId();
            var instituteId = await _repository.GetInstituteIdByActiveUserId(activeUserId.Value);

            var positions = await _positionRepository.GetByIdAsync(request.Id, instituteId);

            if (positions == null)
            {
                _logger.LogWarning($"Position with ID {request.Id} was not found.");
                return false;
            }

            try
            {
                foreach (var position in positions)
                {
                    await _positionRepository.DeleteAsync(position.Id);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting position with ID {request.Id}");
                throw new AppException($"Error deleting position with ID {request.Id}", ex);
            }
        }
    }
}
