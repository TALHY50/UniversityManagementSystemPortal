using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Program;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Models.ModelDto.Program;

namespace UniversityManagementSystemPortal.Application.Handler.Program
{
    public class GetAllProgramsQueryHandler : IRequestHandler<GetAllProgramsQuery, List<ProgramReadDto>>
    {
        private readonly IProgramRepository _programRepository;
        private readonly IMapper _mapper;

        public GetAllProgramsQueryHandler(IProgramRepository programRepository, IMapper mapper)
        {
            _programRepository = programRepository;
            _mapper = mapper;
        }

        public async Task<List<ProgramReadDto>> Handle(GetAllProgramsQuery request, CancellationToken cancellationToken)
        {
            var programs = await _programRepository.GetAllAsync();
            var programss =  _mapper.Map<IEnumerable<ProgramReadDto>>(programs);
            return programss.ToList();
        }
    }
}
