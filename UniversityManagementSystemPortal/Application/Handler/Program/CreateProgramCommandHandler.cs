using AutoMapper;
using MediatR;
using PorgramNamespace = UniversityManagementsystem.Models.Program;

using UniversityManagementSystemPortal.Application.Command.Program;

namespace UniversityManagementSystemPortal
{
    public class CreateProgramCommandHandler : IRequestHandler<CreateProgramCommand, ProgramReadDto>
    {
        private readonly IProgramRepository _repository;
        private readonly IMapper _mapper;

        public CreateProgramCommandHandler(IProgramRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProgramReadDto> Handle(CreateProgramCommand request, CancellationToken cancellationToken)
        {
            var program = _mapper.Map<PorgramNamespace>(request);
            await _repository.AddAsync(program);

            var programReadDto = _mapper.Map<ProgramReadDto>(program);
            return programReadDto;
        }
    }

}
