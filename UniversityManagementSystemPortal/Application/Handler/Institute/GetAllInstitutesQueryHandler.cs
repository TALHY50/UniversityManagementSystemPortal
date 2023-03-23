using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Institute;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.InstituteDto;

namespace UniversityManagementSystemPortal.Application.Handler.Institute
{
    public class GetAllInstitutesQueryHandler : IRequestHandler<GetAllInstitutesQuery, IEnumerable<InstituteDto>>
    {
        private readonly IInstituteRepository _repository;
        private readonly IMapper _mapper;

        public GetAllInstitutesQueryHandler(IInstituteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InstituteDto>> Handle(GetAllInstitutesQuery request, CancellationToken cancellationToken)
        {
            var institutes = await _repository.GetAllAsync();

            var institutesDto = _mapper.Map<IEnumerable<InstituteDto>>(institutes);

            return institutesDto;
        }
    }

}
