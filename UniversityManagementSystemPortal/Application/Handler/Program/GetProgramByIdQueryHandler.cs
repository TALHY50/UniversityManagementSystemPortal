using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Program;
using UniversityManagementSystemPortal.Authorization;

namespace UniversityManagementSystemPortal.Application.Handler.Program
{
    public class GetProgramByIdQueryHandler : IRequestHandler<GetProgramByIdQuery, ProgramReadDto>
    {
        private readonly IProgramRepository _repository;
        private readonly IMapper _mapper;

        public GetProgramByIdQueryHandler(IProgramRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProgramReadDto> Handle(GetProgramByIdQuery request, CancellationToken cancellationToken)
        {
            var program = await _repository.GetByIdAsync(request.ProgramId);
            if (program == null)
            {
                throw new AppException("Program not found.");
            }

            return _mapper.Map<ProgramReadDto>(program);
        }
    }
}
