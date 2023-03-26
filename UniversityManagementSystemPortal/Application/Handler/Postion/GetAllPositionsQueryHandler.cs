using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Position;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Position;

namespace UniversityManagementSystemPortal.Application.Handler.Postion
{
    public class GetAllPositionsQueryHandler : IRequestHandler<GetAllPositionsQuery, List<PositionDto>>
    {
        private readonly IInstituteAdminRepository _repository;
        private readonly IMapper _mapper;
        private readonly IIdentityServices _identityServices;
        private readonly IPositionRepository _positionRepository;

        public GetAllPositionsQueryHandler(IInstituteAdminRepository repository, IMapper mapper, IIdentityServices identityServices, IPositionRepository
        positionRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _identityServices = identityServices;
            _positionRepository = positionRepository;
        }
        public async Task<List<PositionDto>> Handle(GetAllPositionsQuery request, CancellationToken cancellationToken)
        {
            var activeUserId = _identityServices.GetUserId();
            var instituteId = await _repository.GetInstituteIdByActiveUserId(activeUserId.Value);

            var positions = await _positionRepository.GetAllAsync(instituteId.Value);

            var positionDtos = _mapper.Map<IEnumerable<PositionDto>>(positions);

            return positionDtos.ToList();
        }

    }
}