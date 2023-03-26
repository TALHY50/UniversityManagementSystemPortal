using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Command.Position;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Position;

namespace UniversityManagementSystemPortal.Application.Handler.Postion
{
    public class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, PositionDto>
    {
        private readonly IIdentityServices _identityServices;
        private readonly IPositionRepository _positionRepository;
        private readonly IInstituteAdminRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePositionCommandHandler> _logger;

        public CreatePositionCommandHandler(IIdentityServices identityServices, IInstituteAdminRepository repository, IPositionRepository positionRepository, IMapper mapper, ILogger<CreatePositionCommandHandler> logger)
        {
            _identityServices = identityServices;
            _repository = repository;
            _positionRepository = positionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PositionDto> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
        {
            var activeUserId = _identityServices.GetUserId();
            var instituteId = await _repository.GetInstituteIdByActiveUserId(activeUserId.Value);

            if (instituteId == null)
            {
                _logger.LogWarning($"Active user {activeUserId.Value} is not associated with any institute.");
                throw new AppException("Active user is not associated with any institute.");
            }

            var position = _mapper.Map<Position>(request);
            position.Id = Guid.NewGuid();
            position.IsActive = true;
            await _positionRepository.CreateAsync(position);

            var positionDto = _mapper.Map<PositionDto>(position);

            _logger.LogInformation($"Position {positionDto.Id} created for institute {instituteId}.");

            return positionDto;
        }
    }
}
