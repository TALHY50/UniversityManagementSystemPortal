using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfce;
using UniversityManagementSystemPortal.ModelDto.NewFolder;
using UniversityManagementSystemPortal.ModelDto.UserDto;

namespace UniversityManagementSystemPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUserInterface _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserInterface userRepository,
            IMapper mapper, 
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
           
            _userRepository = userRepository;
                _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetUsers()
        { 
            if(_userRepository == null)
            {
                return BadRequest();
            }
       var getUser =    await _userRepository.GetAllAsync();
            return Ok(getUser);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<LoginView>> Login(Login model)
        {
            var response = _userRepository.Authenticate(model);
            return Ok(response);
        }
        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewModel>> GetUser(Guid id)
        {
            if (_userRepository == null)
            {
                return BadRequest();
            }
          var getUser=   await _userRepository.GetByIdAsync(id);
            return Ok(getUser);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<RegistorViewModel>> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var updateUser =  _userRepository.UpdateAsync(user);
            return Ok(updateUser);

        }
     
            [HttpPost]

        public async Task<ActionResult<RegistorViewModel>> Post([FromBody] RegistorViewModel userViewModel)
        {
            if (userViewModel == null)
            {
                return BadRequest();
            }

            var user = _mapper.Map<User>(userViewModel);
            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                // Assign default role to the user
                var defaultRole = await _roleManager.FindByNameAsync("User");
                if (defaultRole != null)
                {
                    await _userManager.AddToRoleAsync(user, defaultRole.Name);
                }

                await _userRepository.RegisterAsUser(user);

                var mappedUserViewModel = _mapper.Map<RegistorViewModel>(user);
                return Ok(mappedUserViewModel);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        _userRepository.DeleteAsync(id);
    }

    private bool UserExists(Guid id)
    {
        return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
    }

}
