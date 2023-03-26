using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Command.Account;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.UserDto;
using UniversityManagementSystemPortal.Models.ModelDto.UserDto;

namespace UniversityManagementSystemPortal.Application.Handler.Account
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegistorUserDto>
    {
        private readonly IUserInterface _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegisterUserCommandHandler> _logger;
        private readonly IIdentityServices _identityServices;

        public RegisterUserCommandHandler(IUserInterface userRepository, IMapper mapper, ILogger<RegisterUserCommandHandler> logger, IIdentityServices identityServices)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _identityServices = identityServices;
        }

        public async Task<RegistorUserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    throw new AppException(nameof(request), "User data is required.");
                }

                var existingUser = await _userRepository.GetByEmailAsync(request.RegisterUserDto.Email);
                if (existingUser != null)
                {
                    throw new AppException($"A user with the email {request.RegisterUserDto.Email} already exists.");
                }

                var user = _mapper.Map<User>(request);
                if (string.IsNullOrEmpty(user.Username))
                {
                    var username = user.Email.Split('@')[0];
                    var uniqueNumber = await _userRepository.GetUniqueUsernameNumberAsync(username);
                    user.Username = $"{username}{uniqueNumber}";
                }
                else
                {
                    var existingUsernameUser = await _userRepository.GetByUsernameAsync(user.Username);
                    if (existingUsernameUser != null)
                    {
                        throw new AppException($"A user with the username {user.Username} already exists.");
                    }
                }

                user.CreatedBy = _identityServices.GetUserId();
                user.LastLoggedIn = DateTime.Now;
                user.UpdatedBy = _identityServices.GetUserId();
                var registeredUser = await _userRepository.RegisterAsUser(user);

                if (registeredUser == null)
                {
                    throw new AppException("Failed to register user.");
                }

                var mappedUserViewModel = _mapper.Map<RegistorUserDto>(registeredUser);
                return mappedUserViewModel;
            }
            catch (AppException ex)
            {
                _logger.LogError(ex, "Error occurred while registering user");
                throw new AppException("Error occurred while registering user");
            }
        }
    }


}
