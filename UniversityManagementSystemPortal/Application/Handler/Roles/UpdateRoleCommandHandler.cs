using AutoMapper;
using MediatR;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Application.Command.Roles;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Role;
using UniversityManagementSystemPortal.Repository;

namespace UniversityManagementSystemPortal.Application.Handler.Roles
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, UpdateRoleDto>
    {
        private readonly IMapper _mapper;
        private readonly IRoleInterface _roleRepository;
        private readonly ILogger<UpdateRoleCommandHandler> _logger;

        public UpdateRoleCommandHandler(IMapper mapper, IRoleInterface roleRepository, ILogger<UpdateRoleCommandHandler> logger)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
            _logger = logger;
        }

        public async Task<UpdateRoleDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _roleRepository.GetByIdAsync(request.Id);

                if (role == null)
                {
                    _logger.LogError($"Role with ID {request.Id} does not exist.");
                    throw new AppException(nameof(Role), request.Id);
                }
                var existingRole = await _roleRepository.GetByRoleTypeAsync(request.RoleType);
                if (existingRole != null && existingRole.Id != request.Id)
                {
                    _logger.LogError($"Role with type {request.RoleType} already exists.");
                    throw new AppException(nameof(Role), $"Role with type {request.RoleType} already exists.");
                }
                _mapper.Map(request, role);
                await _roleRepository.UpdateAsync(role);
                _logger.LogInformation($"Role with ID {request.Id} updated successfully.");
                return _mapper.Map<UpdateRoleDto>(role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the role.");
                throw;
            }
        }
    }
}
