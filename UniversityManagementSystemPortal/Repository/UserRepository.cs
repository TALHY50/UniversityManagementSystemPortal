using Microsoft.Extensions.Options;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfce;
using UniversityManagementSystemPortal.ModelDto;

namespace UniversityManagementSystemPortal.Repository
{

    public class UserRepository : IUserRepository
    {
        private readonly UmspContext _context;
        private IJwtTokenService _jwtTokenService;
        private readonly AppSettings _appSettings;
        public UserRepository(UmspContext context, IJwtTokenService jwtTokenService, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
            _appSettings = appSettings.Value;
        }
        public LoginView Authenticate(Login model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // validate
            if (user == null)
                throw new AppException("Username or password is incorrect");

            // authentication successful
            var jwtToken = _jwtTokenService.GenerateToken(user);

            return new LoginView(user, jwtToken);
        }


        public Task<User> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> Register(User model)
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(User model)
        {
            throw new NotImplementedException();
        }
    }
}
