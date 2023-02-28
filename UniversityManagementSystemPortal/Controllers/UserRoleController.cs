//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using UniversityManagementSystemPortal.RoleManager;

//namespace UniversityManagementSystemPortal.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class UserRoleController : ControllerBase
//    {
//        private readonly IUserRoleManager _userRoleManager;

//        public UserRoleController(IUserRoleManager userRoleManager)
//        {
//            _userRoleManager = userRoleManager;
//        }

//        [HttpPost("{userId}/roles/{roleId}")]
//        public async Task<IActionResult> AddUserRole(Guid userId, Guid roleId)
//        {
//            await _userRoleManager.AddUserRole(userId, roleId);
//            return Ok();
//        }

//        [HttpDelete("{userId}/roles/{roleId}")]
//        public async Task<IActionResult> RemoveUserRole(Guid userId, Guid roleId)
//        {
//            await _userRoleManager.RemoveUserRole(userId, roleId);
//            return Ok();
//        }

//        [HttpGet("{userId}/roles")]
//        public async Task<IActionResult> GetUserRoles(Guid userId)
//        {
//            var roles = await _userRoleManager.GetUserRoles(userId);
//            return Ok(roles);
//        }

//        [HttpGet("{roleId}/users")]
//        public async Task<IActionResult> GetUsersInRole(Guid roleId)
//        {
//            var users = await _userRoleManager.GetUsersInRole(roleId);
//            return Ok(users);
//        }
//    }

//}
