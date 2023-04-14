using MediatR;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.NewFolder;

namespace UniversityManagementSystemPortal.Application.Handler.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginView>
    {
        private readonly IUserInterface _userRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILogger<LoginHandler> _logger;

        public LoginHandler(IUserInterface userRepository, IJwtTokenService jwtTokenService, ILogger<LoginHandler> logger)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
            _logger = logger;
        }

     public async Task<LoginView> Handle(LoginCommand request, CancellationToken cancellationToken)
{
    try
    {
        User user = null;

        if (!string.IsNullOrWhiteSpace(request.model.Email))
        {
            user = await _userRepository.GetByEmailAsync(request.model.Email);
        }
        else if (!string.IsNullOrWhiteSpace(request.model.Username))
        {
            user = await _userRepository.GetByUsernameAsync(request.model.Username);
        }
        else
        {
            _logger.LogWarning("Email and username are both empty.");
            throw new ArgumentException("Please provide either an email or a username.");
        }

        if (user == null)
        {
            _logger.LogWarning("User not found.");
            throw new ArgumentException("User not found.");
        }

        var authenticatedUser = await _userRepository.Authenticate(request.model);

        if (authenticatedUser == null)
        {
            _logger.LogWarning("Invalid password.");
            throw new ArgumentException("Invalid password.");
        }

        var jwtToken = _jwtTokenService.GenerateJwtToken(authenticatedUser);
        var loginView = new LoginView(authenticatedUser, jwtToken);
        return loginView;
    }
    catch (ArgumentException ex)
    {
        _logger.LogError(ex, ex.Message);
        throw;
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "An error occurred while logging in.");
        throw new Exception("An error occurred while logging in. Please try again later.", ex);
    }
}


    }
}