using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Account;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Models.ModelDto.UserDto;

namespace UniversityManagementSystemPortal.Application.Handler.Account
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserViewModel>
    {
        private readonly IUserInterface _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserInterface userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var getUser = await _userRepository.GetByIdAsync(request.UserId);

            if (getUser == null)
            {
                throw new AppException($"User with ID {request.UserId} not found.");
            }

            var userViewModel = _mapper.Map<UserViewModel>(getUser);
            return userViewModel;
        }
    }
}
