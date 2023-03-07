using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Role;
using UniversityManagementSystemPortal.Repository;

namespace UniversityManagementSystemPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleInterface _roleRepository;
        private readonly IMapper _mapper;

        public RolesController(IRoleInterface roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> Get()
        {
            var roles = await _roleRepository.GetAllAsync();
            var roleDtos = _mapper.Map<IEnumerable<RoleDto>>(roles);
            return Ok(roleDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetById(Guid id)
        {
            var role = await _roleRepository.GetByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            var roleDto = _mapper.Map<RoleDto>(role);
            return Ok(roleDto);
        }

        [HttpPost]
        public async Task<ActionResult<RoleDto>> Create(CreateRoleDto createRoleDto)
        {
            var role = _mapper.Map<Role>(createRoleDto);
            await _roleRepository.CreateAsync(role);

            var roleDto = _mapper.Map<RoleDto>(role);
            return CreatedAtAction(nameof(Get), new { id = roleDto.Id }, roleDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateRoleDto updateRoleDto)
        {
            var role = await _roleRepository.GetByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            _mapper.Map(updateRoleDto, role);
            await _roleRepository.UpdateAsync(role);

            return NoContent();
        }

        [HttpDelete("{id}")]
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
        public async Task<ActionResult<RoleDto>> GetByRoleType(int roleType)
        {
            var role = await _roleRepository.GetByRoleTypeAsync(roleType);

            if (role == null)
            {
                return NotFound();
            }

            var roleDto = _mapper.Map<RoleDto>(role);
            return Ok(roleDto);
        }
    }

}
