using AutoMapper;
using MediatR;
using NuGet.Protocol.Core.Types;
using UniversityManagementSystemPortal.Application.Qurey.Program;
using UniversityManagementSystemPortal.Helpers.FilterandSorting;
using UniversityManagementSystemPortal.Helpers.Paging;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Models.ModelDto.Program;

namespace UniversityManagementSystemPortal
{
    public class GetAllProgramsQueryHandler : IRequestHandler<GetAllProgramsQuery, PaginatedList<ProgramReadDto>>
    {
        private readonly IProgramRepository _programRepository;
        private readonly IMapper _mapper;

        public GetAllProgramsQueryHandler(IProgramRepository programRepository, IMapper mapper)
        {
            _programRepository = programRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ProgramReadDto>> Handle(GetAllProgramsQuery request, CancellationToken cancellationToken)
        {
           
            var paginatedViewModel = request.paginatedViewModel;
            var programs = await _programRepository.GetAllAsync();
            var propertyNames = new[] { paginatedViewModel.columnName };
            var filteredPrograms = Filtering.Filter(programs, paginatedViewModel.search, propertyNames);
            var sortedPrograms = Sorting<Program>.Sort(paginatedViewModel.SortBy, paginatedViewModel.columnName, filteredPrograms);
            var paginatedPrograms = PaginationHelper.Create(sortedPrograms, paginatedViewModel);
            var programReadDto = _mapper.Map<PaginatedList<ProgramReadDto>>(paginatedPrograms);
            return await Task.FromResult(programReadDto);
        }
    }
}
