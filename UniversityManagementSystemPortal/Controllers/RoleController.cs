using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Repository;

namespace UniversityManagementSystemPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleInterface _roleRepository;

        public RolesController(IRoleInterface roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> Get()
        {
            var roles = await _roleRepository.GetAllAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetById(Guid id)
        {
            var role = await _roleRepository.GetByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<Role>> Create(Role role)
        {
            await _roleRepository.CreateAsync(role);

            return CreatedAtAction(nameof(Get), new { id = role.Id }, role);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, Role role)
        {
            if (id != role.Id)
            {
                return BadRequest();
            }

            await _roleRepository.UpdateAsync(role);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var role = await _roleRepository.GetByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            await _roleRepository.DeleteAsync(role);

            return NoContent();
        }

        [HttpGet("byRoleType/{roleType}")]
        public async Task<ActionResult<Role>> GetByRoleType(int roleType)
        {
            var role = await _roleRepository.GetByRoleTypeAsync(roleType);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }
    }

}
