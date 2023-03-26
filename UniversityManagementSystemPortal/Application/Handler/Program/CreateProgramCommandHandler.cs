using AutoMapper;
using MediatR;
using PorgramNamespace = UniversityManagementSystemPortal.Program;

using UniversityManagementSystemPortal.Application.Command.Program;
using UniversityManagementSystemPortal.Models.ModelDto.Program;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Application.Handler.Program
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
