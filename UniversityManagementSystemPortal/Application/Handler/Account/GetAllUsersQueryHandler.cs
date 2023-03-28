using AutoMapper;
using MediatR;
using NuGet.Protocol.Core.Types;
using UniversityManagementSystemPortal.Application.Qurey.Account;
using UniversityManagementSystemPortal.Helpers.FilterandSorting;
using UniversityManagementSystemPortal.Helpers.Paging;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Models.ModelDto.UserDto;

namespace UniversityManagementSystemPortal.Application.Handler.Account
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PaginatedList<UserViewModel>>
    {
        private readonly IUserInterface _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUserInterface userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<UserViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            
            var paginatedViewModel = request.paginatedViewModel;
            var getUsers = await _userRepository.GetAllAsync();
            var propertyNames = new[] { paginatedViewModel.columnName };
            var filteredUsers = Filtering.Filter(getUsers, paginatedViewModel.search, propertyNames);
            var sortedUsers = Sorting<User>.Sort(paginatedViewModel.SortBy, paginatedViewModel.columnName, filteredUsers);
            var paginatedUsers = PaginationHelper.Create(sortedUsers, paginatedViewModel);
            var userViewModels = _mapper.Map<PaginatedList<UserViewModel>>(paginatedUsers);
            return await Task.FromResult(userViewModels);
        }
    }

}
