using MediatR;
using UniversityManagementSystemPortal.ModelDto.UserDto;

namespace UniversityManagementSystemPortal.Application.Qurey.Account
{
    public class GetUserByIdQuery : IRequest<UserViewModel>
    {
        public Guid UserId { get; set; }
    }
    
}
