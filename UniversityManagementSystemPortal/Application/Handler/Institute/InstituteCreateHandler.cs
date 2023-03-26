using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Command.Institute;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Institute;
namespace UniversityManagementSystemPortal
{
    public class InstituteCreateHandler : IRequestHandler<InstituteCreateCommand, InstituteCreateDto>
    {
        private readonly IMapper _mapper;
        private readonly IInstituteRepository _repository;
        private readonly IIdentityServices _identityService;

        public InstituteCreateHandler(IMapper mapper, IInstituteRepository repository, IIdentityServices identityService)
        {
            _mapper = mapper;
            _repository = repository;
            _identityService = identityService;
        }

        public async Task<InstituteCreateDto> Handle(InstituteCreateCommand request, CancellationToken cancellationToken)
        {
            var institute = _mapper.Map<Institute>(request);
            institute.IsAutoIncrementAdmissionNo = request.IsAutoIncrementAdmissionNo;

            institute.CreatedBy = _identityService.GetUserId();
            institute.UpdatedBy = _identityService.GetUserId();

            await _repository.AddAsync(institute);

            var instituteDto = _mapper.Map<InstituteCreateDto>(institute);

            return instituteDto;
        }
    }

}
