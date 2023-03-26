using MediatR;
using UniversityManagementSystemPortal.Models.ModelDto.Program;

namespace UniversityManagementSystemPortal.Application.Qurey.Program
{
    public class GetProgramByIdQuery : IRequest<ProgramReadDto>
    {
        public Guid ProgramId { get; set; }
    }
}
