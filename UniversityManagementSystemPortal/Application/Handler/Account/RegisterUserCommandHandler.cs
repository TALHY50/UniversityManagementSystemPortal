using AutoMapper;
using MediatR;
using MimeKit;
using UniversityManagementSystemPortal.Application.Command.Account;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.EmailServices;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.UserDto;
using UniversityManagementSystemPortal.Models.ModelDto.UserDto;

namespace UniversityManagementSystemPortal.Application.Handler.Account
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegistorUserDto>
    {
        public readonly IEmailSender _emailSend ;
        private readonly IUserInterface _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegisterUserCommandHandler> _logger;
        private readonly IIdentityServices _identityServices;

        public RegisterUserCommandHandler(IEmailSender emailSend ,IUserInterface userRepository, IMapper mapper, ILogger<RegisterUserCommandHandler> logger, IIdentityServices identityServices)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _identityServices = identityServices;
            _emailSend = emailSend;
        }

        public async Task<RegistorUserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    throw new AppException(nameof(request), "User data is required.");
                }

                var existingUser = await _userRepository.GetByEmailAsync(request.Email);
                if (existingUser != null)
                {
                    throw new Exception($"A user with the email {request.Email} already exists.");
                }

                var user = _mapper.Map<User>(request);
                if (string.IsNullOrEmpty(user.Username))
                {
                    var username = user.Email.Split('@')[0];
                    if (!username.Any(char.IsDigit))
                    {
                        var random = new Random();
                        var uniqueNumber = random.Next(100, 999);
                        while (await _userRepository.GetByUsernameAsync($"{username}{uniqueNumber}") != null)
                        {
                            uniqueNumber = random.Next(100, 999);
                        }
                        username = $"{username}{uniqueNumber}";
                    }
                    var existingUsernameUser = await _userRepository.GetByUsernameAsync(username);
                    if (existingUsernameUser != null)
                    {
                        throw new AppException($"A user with the username {username} already exists.");
                    }
                    user.Username = username;
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
                var message = new Message(new List<string> { user.Email },
                           "Registration successful",
                           $"Dear {user.FirstName},<br /><br />Thank you for registering on our website.<br /><br />Best regards,<br />The University Management System Portal Team");

                _emailSend.SendEmail(message);
                _logger.LogDebug($"Sending email to {message.To}");

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
