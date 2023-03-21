using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Roles;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Role;
using UniversityManagementSystemPortal.Repository;

namespace UniversityManagementSystemPortal.Application.Handler.Roles
{
    public class GetRoleByTypeQueryHandler : IRequestHandler<GetRoleByTypeQuery, RoleDto>
    {
        private readonly IRoleInterface _roleRepository;
        private readonly IMapper _mapper;

        public GetRoleByTypeQueryHandler(IRoleInterface roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<RoleDto> Handle(GetRoleByTypeQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByRoleTypeAsync(request.RoleType);

            if (role == null)
            {
                throw new AppException($"Role with type {request.RoleType} was not found.");
            }

            return _mapper.Map<RoleDto>(role);
        }
    }

}
