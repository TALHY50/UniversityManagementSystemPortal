using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Interfce;
using UniversityManagementSystemPortal.ModelDto.UserRoleDto;

namespace UniversityManagementSystemPortal.Controllers
{
    [ApiController]
    [Route("api/userroles")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleInterface _userRoleRepository;
        private readonly IUserInterface _userRepository;

        public UserRoleController(IUserRoleInterface userRoleRepository,
            IUserInterface userRepository)
        {
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRole>>> GetAllUserRoles()
        {
            var userRoles = await _userRoleRepository.GetAllAsync();
            return Ok(userRoles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserRole>> GetUserRoleById(Guid id)
        {
            var userRole = await _userRoleRepository.GetByIdAsync(id);
            if (userRole == null)
            {
                return NotFound();
            }
            return Ok(userRole);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserRole([FromBody] UserRoleDto userRoleDto)
        {
            var user = await _userRepository.GetByIdAsync(userRoleDto.Id);
            if (user == null)
            {
                return BadRequest(new { message = "User not found." });
            }

            await _userRoleRepository.AddUserRoleAsync(userRoleDto.UserId, userRoleDto.RoleId);

            return Ok(new { message = "User role created." });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserRole(Guid id, [FromBody] UserRoleDto userRoleDto)
        {
            var userRole = await _userRoleRepository.GetByIdAsync(id);
            if (userRole == null)
            {
                return NotFound(new { message = "User role not found." });
            }

            userRole.RoleId = userRoleDto.RoleId;
            userRole.UserId = userRoleDto.UserId;

            await _userRoleRepository.UpdateAsync(userRole);

            return Ok(new { message = "User role updated." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveUserRole(Guid id)
        {
            var userRole = await _userRoleRepository.GetByIdAsync(id);
            if (userRole == null)
            {
                return NotFound(new { message = "User role not found." });
            }

            await _userRoleRepository.RemoveUserRoleAsync(userRole.RoleId, userRole.UserId);

            return Ok(new { message = "User role removed." });
        }
    }




}
