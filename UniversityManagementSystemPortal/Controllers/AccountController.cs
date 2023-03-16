using AutoMapper;
using ExcelDataReader.Log;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Authorization.UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfce;
using Serilog;
using UniversityManagementSystemPortal.ModelDto.UserDto;
using AllowAnonymous = UniversityManagementSystemPortal.Authorization.AllowAnonymousAttribute;
using UniversityManagementSystemPortal.ModelDto.NewFolder;

namespace UniversityManagementSystemPortal.Controllers
{
    [JwtAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserInterface _userRepository;
        private readonly IIdentityServices _identityServices;
        private readonly IMapper _mapper;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserInterface userRepository,
            IMapper mapper,
            IJwtTokenService jwtTokenService,
            IIdentityServices identityServices,
            ILogger<AccountController> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
            _identityServices = identityServices;
            _logger = logger;
        }

        [JwtAuthorize("Admin", "SuperAdmin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> Get()
        {
            try
            {
                var getUsers = await _userRepository.GetAllAsync();
                var userViewModels = _mapper.Map<IEnumerable<UserViewModel>>(getUsers);
                return Ok(new { message = "Successfully retrieved all users.", data = userViewModels });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all users");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error occurred while getting all users" });
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewModel>> GetById(Guid id)
        {
            try
            {
                var getUser = await _userRepository.GetByIdAsync(id);

                if (getUser == null)
                {
                    return NotFound(new { message = $"User with ID {id} not found." });
                }

                var userViewModel = _mapper.Map<UserViewModel>(getUser);
                return Ok(new { message = "Successfully retrieved user.", data = userViewModel });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting user with ID {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"Error occurred while getting user with ID {id}" });
            }
        }

        [AllowAnonymous]
        [HttpPost("post")]
        public async Task<ActionResult<RegistorViewModel>> Post([FromForm] RegistorViewModel userViewModel)
        {
            try
            {
                if (userViewModel == null)
                {
                    return BadRequest(new { message = "User data is required." });
                }
                var user = _mapper.Map<User>(userViewModel);
                user.CreatedBy = _identityServices.GetUserId();
                user.LastLoggedIn = DateTime.Now;
                user.UpdatedBy = _identityServices.GetUserId();
                var registeredUser = await _userRepository.RegisterAsUser(user);

                if (registeredUser == null)
                {
                    return BadRequest(new { message = "Failed to register user." });
                }

                var mappedUserViewModel = _mapper.Map<RegistorViewModel>(registeredUser);
                return Ok(new { message = "Successfully registered user.", data = mappedUserViewModel });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error occurred while registering user" });
            }
        }
        [JwtAuthorize("Admin", "SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<RegistorViewModel>> Put([FromForm] Guid id, User user)

        {
            try
            {
                if (id != user.Id)
                {
                    return BadRequest(new { message = $"User ID in request body ({user.Id}) does not match ID in URL ({id})." });
                }

                var userId = _identityServices.GetUserId();
                if (userId == null)
                {
                    return BadRequest(new { message = "User ID could not be retrieved." });
                }

                user.UpdatedBy = userId;
                var updatedUser =  _userRepository.UpdateAsync(user);

                if (updatedUser == null)
                {
                    return BadRequest(new { message = "Failed to update user." });
                }

                var updatedUserViewModel = _mapper.Map<RegistorViewModel>(updatedUser);
                return Ok(new { message = "Successfully updated user.", data = updatedUserViewModel });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating user: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to update user." });
            }
        }
        [JwtAuthorize("SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var userToDelete = await _userRepository.GetByIdAsync(id);

                if (userToDelete == null)
                {
                    return NotFound(new { message = $"User with ID {id} not found." });
                }

                await _userRepository.DeleteAsync(userToDelete);

                _logger.LogInformation("User with ID {UserId} deleted successfully.", id);

                return Ok(new { message = "Successfully deleted user." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user with ID {UserId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while deleting the user." });
            }
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<LoginView>> Login(Login model)
        {
            try
            {
                var user = await _userRepository.Authenticate(model);

                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid username or password." });
                }

                var jwtToken = _jwtTokenService.GenerateJwtToken(user);
                var loginView = new LoginView(user, jwtToken);
                return Ok(new { message = "Successfully logged in.", data = loginView });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while logging in. Please try again later." });
            }
        }

    }
}