using MediatR;
using UniversityManagementSystemPortal.Models.ModelDto.Position;

namespace UniversityManagementSystemPortal.Application.Handler.Postion
{
    public record GetLookupPositionList(LookupPositiondto Positiondto): IRequest<List<LookupPositiondto>>;
 
}
