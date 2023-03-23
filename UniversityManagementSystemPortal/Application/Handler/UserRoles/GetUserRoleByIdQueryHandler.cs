using AutoMapper;
using MediatR;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Application.Qurey.UserRoles;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.UserRoleDto;

namespace UniversityManagementSystemPortal
{
    public class GetUserRoleByIdQueryHandler : IRequestHandler<GetUserRoleByIdQuery, UserRoleDto>
    {
        private readonly IUserRoleRepository _repository;
        private readonly IMapper _mapper;

        public GetUserRoleByIdQueryHandler(IUserRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserRoleDto> Handle(GetUserRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var userRole = await _repository.GetByIdAsync(request.Id);

            if (userRole == null)
            {
                throw new AppException(nameof(UserRole), request.Id);
            }

            var userRoleDto = _mapper.Map<UserRoleDto>(userRole);

            return userRoleDto;
        }
    }
}
