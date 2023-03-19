using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Command.Roles;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Application.Handler.Roles
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Unit>
    {
        private readonly IRoleInterface _roleRepository;

        public DeleteRoleCommandHandler(IRoleInterface roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByIdAsync(request.Id);

            if (role == null)
            {
                throw new AppException($"Role with ID {request.Id} not found.");
            }

            await _roleRepository.DeleteAsync(role);

            return Unit.Value;
        }
    }
}
