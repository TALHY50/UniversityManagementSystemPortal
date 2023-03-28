using MediatR;
using UniversityManagementSystemPortal.Helpers.Paging;
using UniversityManagementSystemPortal.Models.ModelDto.UserDto;

namespace UniversityManagementSystemPortal.Application.Qurey.Account
{
    public class GetAllUsersQuery : IRequest<PaginatedList<UserViewModel>>
    {
        public PaginatedViewModel? paginatedViewModel { get; set; }
    }
}
