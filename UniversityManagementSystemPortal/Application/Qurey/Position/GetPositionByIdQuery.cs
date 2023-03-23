using MediatR;
using UniversityManagementSystemPortal.ModelDto.Position;

namespace UniversityManagementSystemPortal.Application.Qurey.Position
{
    public class GetPositionByIdQuery : IRequest<List<PositionDto>>
    {
        public Guid Id { get; set; }
        public Guid? InstituteId { get; set; }
    }

}
