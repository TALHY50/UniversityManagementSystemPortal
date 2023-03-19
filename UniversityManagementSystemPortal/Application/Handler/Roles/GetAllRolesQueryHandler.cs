using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Roles;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Role;

namespace UniversityManagementSystemPortal.Application.Handler.Roles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<RoleDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRoleInterface _roleRepository;

        public GetAllRolesQueryHandler(IRoleInterface roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetAllAsync();
            var roleDtos = _mapper.Map<IEnumerable<RoleDto>>(roles);
            return roleDtos;
        }
    }
}
