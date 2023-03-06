//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using UniversityManagementsystem.Models;
//using UniversityManagementSystemPortal.Interfaces;
//using UniversityManagementSystemPortal.Interfce;
//using UniversityManagementSystemPortal.ModelDto.UserRoleDto;

//namespace UniversityManagementSystemPortal.Controllers
//{
//    [ApiController]
//    [Route("api/userroles")]
//    public class UserRoleController : ControllerBase
//    {
//        private readonly UserManager<User> _userManager;
//        private readonly RoleManager<Role> _roleManager;
//        private readonly IUserRoleInterface _userRoleRepository;
//        private readonly IUserInterface _userRepository;

//        public UserRoleController(IUserRoleInterface userRoleRepository,
//            IUserInterface userRepository,
//            UserManager<User> userManager,
//            RoleManager<Role> roleManager)
//        {
//            _userManager = userManager;
//            _roleManager = roleManager;
//            _userRoleRepository = userRoleRepository;
//            _userRepository = userRepository;
//        }

//        // GET api/userroles
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<UserRole>>> GetAllUserRoles()
//        {
//            var userRoles = await _userRoleRepository.GetAllAsync();
//            return Ok(userRoles);
//        }

//        // GET api/userroles/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<UserRole>> GetUserRoleById(Guid id)
//        {
//            var userRole = await _userRoleRepository.GetByIdAsync(id);
//            if (userRole == null)
//            {
//                return NotFound();
//            }
//            return Ok(userRole);
//        }

//        // POST api/userroles
//        [HttpPost]
//        public async Task<ActionResult<UserRole>> AddUserRole([FromBody] UserRoleDto userRoleDto)
//        {
//            var user = await _userRepository.GetByIdAsync(userRoleDto.Id);
//            if (user == null)
//            {
//                return BadRequest("User not found.");
//            }

//            await _userRoleRepository.AddUserRoleAsync(userRoleDto.RoleId, userRoleDto.UserId);

//            var createdUserRole = await _userRoleRepository.GetByUserRoleAsync(userRoleDto.RoleId, userRoleDto.UserId);
//            return CreatedAtAction(nameof(GetUserRoleById), new { id = createdUserRole.Id }, createdUserRole);
//        }

//        // PUT api/userroles/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateUserRole(Guid id, [FromBody] UserRoleDto userRoleDto)
//        {
//            var userRole = await _userRoleRepository.GetByIdAsync(id);
//            if (userRole == null)
//            {
//                return NotFound();
//            }

//            userRole.RoleId = userRoleDto.RoleId;
//            userRole.UserId = userRoleDto.UserId;

//            await _userRoleRepository.UpdateAsync(userRole);

//            return NoContent();
//        }

//        // DELETE api/userroles/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> RemoveUserRole(Guid id)
//        {
//            var userRole = await _userRoleRepository.GetByIdAsync(id);
//            if (userRole == null)
//            {
//                return NotFound();
//            }

//            await _userRoleRepository.RemoveUserRoleAsync(userRole.RoleId, userRole.UserId);

//            return NoContent();
//        }
//    }



//}
