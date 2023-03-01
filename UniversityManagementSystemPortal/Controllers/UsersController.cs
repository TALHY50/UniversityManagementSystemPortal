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
        private readonly UmspContext _context;
        private readonly IUserRepository _userRepository;
        public UsersController(UmspContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
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
        public async Task<ActionResult<RegistorViewModel>> Post(RegistorViewModel user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            await _userRepository.RegisterAsUser(user);
            return user;
        }
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUser(Guid id)
        //{
        //    _userRepository.DeleteAsync(id);
        //}

        //private bool UserExists(Guid id)
        //{
        //    return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
