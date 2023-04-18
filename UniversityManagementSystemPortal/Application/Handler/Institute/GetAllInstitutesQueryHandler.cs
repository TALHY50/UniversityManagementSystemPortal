using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Institute;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.InstituteDto;
using UniversityManagementSystemPortal.Models.ModelDto.Institute;

namespace UniversityManagementSystemPortal.Application.Handler.Institute
{
    public class GetAllInstitutesQueryHandler : IRequestHandler<GetAllInstitutesQuery, List<InstituteDto>>
    {
        private readonly IInstituteRepository _repository;
        private readonly IMapper _mapper;

        public GetAllInstitutesQueryHandler(IInstituteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<InstituteDto>> Handle(GetAllInstitutesQuery request, CancellationToken cancellationToken)
        {
            var institutes =  _repository.GetAllAsync();

            var institutesDto = _mapper.Map<List<InstituteDto>>(institutes);

            return institutesDto;
        }

    }

}
