using MediatR;

namespace UniversityManagementSystemPortal.Application.Qurey.Program
{
    public class GetAllProgramsQuery : IRequest<List<ProgramReadDto>>
    {
    }
}
