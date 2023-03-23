using AutoMapper;
using MediatR;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Application.Command.UserRoles;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.UserRoleDto;

namespace UniversityManagementSystemPortal.Application.Handler.UserRoles
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserRoleDto>
    {
        private readonly IUserRoleRepository _repository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserRoleDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userRole = _mapper.Map<UserRole>(request.CreateUserRoleDto);

            await _repository.AddAsync(userRole);

            var userRoleDto = _mapper.Map<UserRoleDto>(userRole);

            return userRoleDto;
        }
    }
}
