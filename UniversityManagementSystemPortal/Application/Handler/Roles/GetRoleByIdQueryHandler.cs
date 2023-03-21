using AutoMapper;
using MediatR;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Application.Qurey.Roles;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Role;
using UniversityManagementSystemPortal.Repository;

namespace UniversityManagementSystemPortal.Application.Handler.Roles
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto>
    {
        private readonly IRoleInterface _roleRepository;
        private readonly IMapper _mapper;

        public GetRoleByIdQueryHandler(IRoleInterface roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(request.Id);

            if (role == null)
            {
                throw new AppException(nameof(Role), request.Id);
            }

            return _mapper.Map<RoleDto>(role);
        }
    }

}
