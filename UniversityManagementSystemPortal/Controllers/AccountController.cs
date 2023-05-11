using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystemPortal.Application.Command.Account;
using UniversityManagementSystemPortal.Application.Qurey.Account;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Authorization.UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Helpers.Paging;
using UniversityManagementSystemPortal.ModelDto.NewFolder;
using UniversityManagementSystemPortal.ModelDto.UserDto;
using UniversityManagementSystemPortal.Models.ModelDto.UserDto;
using AllowAnonymous = UniversityManagementSystemPortal.Authorization.AllowAnonymousAttribute;

namespace UniversityManagementSystemPortal.Controllers
{
    [JwtAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            IMediator mediator,
            ILogger<AccountController> logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [JwtAuthorize("Admin", "SuperAdmin")]
        [HttpGet]
        public async Task<ActionResult<List<UserViewModel>>> Get([FromQuery] PaginatedViewModel paginatedView)
        {
            try
            {
                var userViewModels = await _mediator.Send(new GetAllUsersQuery { paginatedViewModel = paginatedView });
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
                var userViewModel = await _mediator.Send(new GetUserByIdQuery { UserId = id });
                return Ok(new { message = "Successfully retrieved user.", data = userViewModel });
            }
            catch (AppException ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting user with ID {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"Error occurred while getting user with ID {id}" });
            }
        }
        [JwtAuthorize("Admin", "SuperAdmin")]
        [HttpPost("post")]
        public async Task<IActionResult> Register([FromBody] RegistorUserDto registerUserDto)
        {
            try
            {
                var command = registerUserDto;
                var result = await _mediator.Send(new RegisterUserCommand(registerUserDto));
                return Ok(result);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [JwtAuthorize("Admin", "SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<UserViewModel>> UpdateUser(Guid id, [FromForm] UpdateUserDto updateUserDto)
        {
            var command = new UpdateUserCommand { Id = id, UpdateUserDto = updateUserDto };
            var user = await _mediator.Send(command);

            return Ok(user);
        }
        [JwtAuthorize("Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteUserCommand { UserId = id });

                _logger.LogInformation("User with ID {UserId} deleted successfully.", id);

                return Ok(new { message = "Successfully deleted user." });
            }
            catch (AppException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user with ID {UserId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while deleting the user." });
            }
        }
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<ActionResult<LoginView>> Login(Login model)
        {
            try
            {
                var loginView = await _mediator.Send(new LoginCommand(model));
                return Ok(new { message = "Successfully logged in.", loginView });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while logging in. Please try again later." });
            }
        }

    }
}