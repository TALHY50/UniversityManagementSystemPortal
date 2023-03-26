using MediatR;
using UniversityManagementSystemPortal.Models.ModelDto.UserDto;

namespace UniversityManagementSystemPortal.Application.Qurey.Account
{
    public class GetUserByIdQuery : IRequest<UserViewModel>
    {
        public Guid UserId { get; set; }
    }
    
}
