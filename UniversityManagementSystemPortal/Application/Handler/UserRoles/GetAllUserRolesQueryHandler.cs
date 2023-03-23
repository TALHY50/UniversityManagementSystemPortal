using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.UserRoles;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.UserRoleDto;

namespace UniversityManagementSystemPortal.Application.Handler.UserRoles
{
    public class GetAllUserRolesQueryHandler : IRequestHandler<GetAllUserRolesQuery, List<UserRoleDto>>
    {
        private readonly IUserRoleRepository _repository;
        private readonly IMapper _mapper;

        public GetAllUserRolesQueryHandler(IUserRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<UserRoleDto>> Handle(GetAllUserRolesQuery request, CancellationToken cancellationToken)
        {
            var userRoles = await _repository.GetAllAsync();

            var userRoleDtos = _mapper.Map<IEnumerable<UserRoleDto>>(userRoles);

            return userRoleDtos.ToList();
        }
    }
}
