using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Position;
using UniversityManagementSystemPortal.Helpers.FilterandSorting;
using UniversityManagementSystemPortal.Helpers.Paging;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Position;

namespace UniversityManagementSystemPortal
{ 
    public class GetAllPositionsQueryHandler : IRequestHandler<GetAllPositionsQuery, PaginatedList<PositionDto>>
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
        public async Task<PaginatedList<PositionDto>> Handle(GetAllPositionsQuery request, CancellationToken cancellationToken)
        {
            var activeUserId = _identityServices.GetUserId();
            var instituteId = await _repository.GetInstituteIdByActiveUserId(activeUserId.Value);
            var paginatedViewModel = request.paginatedViewModel;
            var positions = await _positionRepository.GetAllAsync(instituteId.Value);
            var propertyNames = new[] { paginatedViewModel.columnName };
            var filteredPositions = Filtering.Filter(positions, paginatedViewModel.search, propertyNames);
            var sortedCategories = Sorting<Position>.Sort(paginatedViewModel.SortBy, paginatedViewModel.columnName, filteredPositions);
            var paginatedCategories = PaginationHelper.Create(sortedCategories, paginatedViewModel);
            var categoryDto = _mapper.Map<PaginatedList<PositionDto>>(paginatedCategories);
            return await Task.FromResult(categoryDto);
        }

    }
}