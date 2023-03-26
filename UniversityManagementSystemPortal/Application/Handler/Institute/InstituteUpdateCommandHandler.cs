using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Command.Institute;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Models.ModelDto.Institute;

namespace UniversityManagementSystemPortal.Application.Handler.Institute
{
    public class InstituteUpdateCommandHandler : IRequestHandler<InstituteUpdateCommand, InstituteUpdateDto>
    {
        private readonly IMapper _mapper;
        private readonly IInstituteRepository _repository;
        private readonly IIdentityServices _identityServices;
        private readonly ILogger<InstituteUpdateCommandHandler> _logger;

        public InstituteUpdateCommandHandler(IMapper mapper, IInstituteRepository repository, IIdentityServices identityServices, ILogger<InstituteUpdateCommandHandler> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _identityServices = identityServices;
            _logger = logger;
        }

        public async Task<InstituteUpdateDto> Handle(InstituteUpdateCommand request, CancellationToken cancellationToken)
        {
            var institute = await _repository.GetByIdAsync(request.Id);

            if (institute == null)
            {
                throw new AppException($"Institute with Id {request.Id} not found.");
            }

            _mapper.Map(request, institute);
            institute.IsAutoIncrementAdmissionNo = request.IsAutoIncrementAdmissionNo;

            institute.UpdatedBy = _identityServices.GetUserId();
            institute.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _repository.UpdateAsync(institute);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating institute with Id {request.Id}: {ex.Message}", ex);
            }

            return _mapper.Map<InstituteUpdateDto>(institute);
        }

    }
}
