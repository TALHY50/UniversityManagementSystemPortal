using MediatR;
using UniversityManagementSystemPortal.ModelDto.Position;

namespace UniversityManagementSystemPortal.Application.Command.Position
{
    public class UpdatePositionCommand : IRequest
    {
        public Guid Id { get; set; }
        public PositionAddorUpdateDto PositionUpdateDto { get; set; }
        public UpdatePositionCommand(Guid id, PositionAddorUpdateDto positionUpdateDto)
        {
            Id = id;
            PositionUpdateDto = positionUpdateDto;
        }
    }

}
