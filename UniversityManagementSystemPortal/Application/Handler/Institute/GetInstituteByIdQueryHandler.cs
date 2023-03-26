using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Institute;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.InstituteDto;
using UniversityManagementSystemPortal.Models.ModelDto.Institute;

namespace UniversityManagementSystemPortal.Application.Handler.Institute
{
    public class GetInstituteByIdQueryHandler : IRequestHandler<GetInstituteByIdQuery, InstituteDto>
    {
        private readonly IInstituteRepository _repository;
        private readonly IMapper _mapper;

        public GetInstituteByIdQueryHandler(IInstituteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<InstituteDto> Handle(GetInstituteByIdQuery request, CancellationToken cancellationToken)
        {
            var institute = await _repository.GetByIdAsync(request.Id);

            if (institute == null)
            {
                throw new AppException(nameof(Institute), request.Id);
            }

            var instituteDto = _mapper.Map<InstituteDto>(institute);

            return instituteDto;
        }
    }

}
