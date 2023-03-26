using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Department;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Models.ModelDto.Department;

namespace UniversityManagementSystemPortal.Application.Handler.Department
{
    public class GetDepartmentsByInstituteIdQueryHandler : IRequestHandler<GetDepartmentsByInstituteIdQuery, IEnumerable<DepartmentDto>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public GetDepartmentsByInstituteIdQueryHandler(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentDto>> Handle(GetDepartmentsByInstituteIdQuery request, CancellationToken cancellationToken)
        {
            var departments = await _departmentRepository.GetDepartmentsByInstituteIdAsync(request.InstituteId);
            var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return departmentDtos;
        }
    }
}
