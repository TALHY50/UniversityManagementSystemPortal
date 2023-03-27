using MediatR;
using UniversityManagementSystemPortal.Helpers.Paging;
using UniversityManagementSystemPortal.ModelDto.Position;

namespace UniversityManagementSystemPortal.Application.Qurey.Position
{
    public class GetAllPositionsQuery : IRequest<PaginatedList<PositionDto>>
    {
        public PaginatedViewModel? paginatedViewModel { get; set; }
    }
}
