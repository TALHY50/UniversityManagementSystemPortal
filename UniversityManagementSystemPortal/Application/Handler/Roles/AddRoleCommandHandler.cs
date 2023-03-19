using AutoMapper;
using MediatR;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Role;

namespace UniversityManagementSystemPortal
{
    public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, AddRoleDto>
    {
        private readonly IMapper _mapper;
        private readonly IRoleInterface _roleRepository;
        private readonly ILogger<AddRoleCommandHandler> _logger;

        public AddRoleCommandHandler(IMapper mapper, IRoleInterface roleRepository, ILogger<AddRoleCommandHandler> logger)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
            _logger = logger;
        }

        public async Task<AddRoleDto> Handle(AddRoleCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var role = _mapper.Map<Role>(command);
                await _roleRepository.CreateAsync(role);

                var roleDto = _mapper.Map<AddRoleDto>(role);
                _logger.LogInformation($"Role added successfully");
                return roleDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while adding the role with name {command.Name}");
                throw new AppException(nameof(Role), $"An error occurred while adding the role with name {command.Name}");
            }
        }
    }

}
