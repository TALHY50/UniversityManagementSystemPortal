using MediatR;
using UniversityManagementSystemPortal.ModelDto.UserRoleDto;

namespace UniversityManagementSystemPortal.Application.Qurey.UserRoles
{
    public class GetUserRoleByIdQuery : IRequest<UserRoleDto>
    {
        public Guid Id { get; set; }
    }
}
