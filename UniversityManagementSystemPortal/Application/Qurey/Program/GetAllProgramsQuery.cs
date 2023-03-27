using MediatR;
using UniversityManagementSystemPortal.Helpers.Paging;
using UniversityManagementSystemPortal.Models.ModelDto.Program;

namespace UniversityManagementSystemPortal.Application.Qurey.Program
{
    public class GetAllProgramsQuery : IRequest<PaginatedList<ProgramReadDto>>
    {
        public PaginatedViewModel? paginatedViewModel { get; set; }
    }
}
