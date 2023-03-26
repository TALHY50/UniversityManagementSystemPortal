using MediatR;
using UniversityManagementSystemPortal.Models.ModelDto.Program;

namespace UniversityManagementSystemPortal.Application.Qurey.Program
{
    public class GetAllProgramsQuery : IRequest<List<ProgramReadDto>>
    {
    }
}
