using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Account;
using UniversityManagementSystemPortal.Interfce;
using UniversityManagementSystemPortal.ModelDto.UserDto;

namespace UniversityManagementSystemPortal.Application.Handler.Account
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserViewModel>>
    {
        private readonly IUserInterface _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUserInterface userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var getUsers = await _userRepository.GetAllAsync();
            var userViewModels = _mapper.Map<IEnumerable<UserViewModel>>(getUsers);
            return userViewModels.ToList();
        }
    }

}
