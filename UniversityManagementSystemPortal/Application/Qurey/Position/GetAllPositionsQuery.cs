using MediatR;
using UniversityManagementSystemPortal.ModelDto.Position;

namespace UniversityManagementSystemPortal.Application.Qurey.Position
{
    public class GetAllPositionsQuery : IRequest<List<PositionDto>>
    {
    }
}
