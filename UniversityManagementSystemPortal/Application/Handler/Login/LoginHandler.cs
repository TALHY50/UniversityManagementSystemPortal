using MediatR;
using UniversityManagementSystemPortal.Application.Command.Account;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfce;
using UniversityManagementSystemPortal.ModelDto.NewFolder;
using UniversityManagementSystemPortal.Repository;

namespace UniversityManagementSystemPortal.Application.Handler.Account
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
                var user = await _userRepository.Authenticate(request.model);

                if (user == null)
                {
                    throw new Exception("Invalid username or password.");
                }

                var jwtToken = _jwtTokenService.GenerateJwtToken(user);
                var loginView = new LoginView(user, jwtToken);
                return loginView;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in.");
                throw new Exception("An error occurred while logging in. Please try again later.", ex);
            }
        }
    }
}
