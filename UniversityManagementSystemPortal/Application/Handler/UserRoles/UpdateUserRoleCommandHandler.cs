using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Command.UserRoles;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfaces;
namespace UniversityManagementSystemPortal.Application.Handler.UserRoles
{
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand>
    {
        private readonly IUserRoleRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUserRoleCommandHandler> _logger;

        public UpdateUserRoleCommandHandler(IUserRoleRepository repository, IMapper mapper, ILogger<UpdateUserRoleCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var userRole = await _repository.GetByIdAsync(request.Id);

            if (userRole == null)
            {
                _logger.LogWarning("User role with id {Id} not found.", request.Id);
                throw new AppException(nameof(UserRole), request.Id);
            }

            _mapper.Map(request.UserRoleUpdateDto, userRole);

            await _repository.UpdateAsync(userRole);

            _logger.LogInformation("User role with id {Id} updated successfully.", request.Id);

            return Unit.Value;
        }
    }

}
