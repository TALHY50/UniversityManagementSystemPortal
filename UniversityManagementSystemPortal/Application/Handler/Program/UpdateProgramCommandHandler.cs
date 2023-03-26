using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Command.Program;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Application.Handler.Program
{
    public class UpdateProgramCommandHandler : IRequestHandler<UpdateProgramCommand>
    {
        private readonly IProgramRepository _repository;
        private readonly IMapper _mapper;
        private readonly IIdentityServices _identityService;

        public UpdateProgramCommandHandler(IProgramRepository repository, IMapper mapper, IIdentityServices identityService)
        {
            _repository = repository;
            _mapper = mapper;
            _identityService = identityService;
        }

        public async Task<Unit> Handle(UpdateProgramCommand request, CancellationToken cancellationToken)
        {
            var program = await _repository.GetByIdAsync(request.Id);
            if (program == null)
            {
                throw new AppException("Program not found.");
            }

            request.UpdatedBy = _identityService.GetUserId().Value;

            _mapper.Map(request, program);
            await _repository.UpdateAsync(program);

            return Unit.Value;
        }
    }

}
