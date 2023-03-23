using MediatR;
using UniversityManagementSystemPortal.ModelDto.Position;

namespace UniversityManagementSystemPortal.Application.Command.Position
{
    public class CreatePositionCommand : IRequest<PositionDto>
    {
        public string Name { get; set; }
        public Guid? CategoryId { get; set; }
        public bool IsActive { get; set; }
    }

}