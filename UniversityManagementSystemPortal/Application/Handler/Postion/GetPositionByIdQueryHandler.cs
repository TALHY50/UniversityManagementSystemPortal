using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Position;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Position;

namespace UniversityManagementSystemPortal.Application.Handler.Postion
{
    public class GetPositionByIdQueryHandler : IRequestHandler<GetPositionByIdQuery, List<PositionDto>>
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;
        private readonly IIdentityServices _identityServices;
        private readonly IInstituteAdminRepository _repository;

        public GetPositionByIdQueryHandler(IInstituteAdminRepository repository, IPositionRepository positionRepository, IMapper mapper, IIdentityServices identityServices)
        {
            _repository = repository;
            _mapper = mapper;
            _identityServices = identityServices;
            _positionRepository = positionRepository;
        }

        public async Task<List<PositionDto>> Handle(GetPositionByIdQuery request, CancellationToken cancellationToken)
        {
            var activeUserId = _identityServices.GetUserId();
            var instituteId = await _repository.GetInstituteIdByActiveUserId(activeUserId.Value);

            var positions = await _positionRepository.GetByIdAsync(request.Id, instituteId);

            if (positions == null || !positions.Any())
            {
                return null;
            }

            var positionDtos = _mapper.Map<IEnumerable<PositionDto>>(positions);
            return positionDtos.ToList();
        }
    }



}
