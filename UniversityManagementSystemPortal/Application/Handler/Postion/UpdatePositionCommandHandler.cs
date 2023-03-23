using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Command.Position;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Application.Handler.Postion
{
    public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand>
    {
        private readonly IInstituteAdminRepository _instituteAdminRepository;
        private readonly IPositionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePositionCommandHandler> _logger;
        private readonly IIdentityServices _identityServices;

        public UpdatePositionCommandHandler(IInstituteAdminRepository instituteAdminRepository, IPositionRepository repository, IMapper mapper, ILogger<UpdatePositionCommandHandler> logger, IIdentityServices identityServices)
        {
            _instituteAdminRepository = instituteAdminRepository;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _identityServices = identityServices;
        }

        public async Task<Unit> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
        {
            var activeUserId = _identityServices.GetUserId();
            var activeUserInstituteId = await _instituteAdminRepository.GetInstituteIdByActiveUserId(activeUserId.Value);

            var positions = await _repository.GetByIdAsync(request.Id, activeUserInstituteId);

            if (positions == null )
            {
                _logger.LogError("Position with id {id} not found or you are not authorized to update this position.", request.Id);
                throw new AppException("Position not found or you are not authorized to update this position.");
            }

            var position = positions.FirstOrDefault();

            _mapper.Map(request.PositionUpdateDto, position);

            await _repository.UpdateAsync(position, activeUserInstituteId);

            _logger.LogInformation("Position with id {id} updated successfully.", request.Id);

            return Unit.Value;
        }

    }
}
