using AutoMapper;
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
    [Route("api/[controller]")]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRoleRepository _repository;
        private readonly IMapper _mapper;

        public UserRolesController(IUserRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserRoleDto>> GetByIdAsync(Guid id)
        {
            var userRole = await _repository.GetByIdAsync(id);

            if (userRole == null)
            {
                return NotFound();
            }

            var userRoleDto = _mapper.Map<UserRoleDto>(userRole);

            return userRoleDto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRoleDto>>> GetAllAsync()
        {
            var userRoles = await _repository.GetAllAsync();

            var userRoleDtos = _mapper.Map<IEnumerable<UserRoleDto>>(userRoles);

            return Ok(userRoleDtos);
        }

        [HttpPost]
        public async Task<ActionResult<UserRoleDto>> CreateAsync([FromQuery]CreateUserRoleDto createUserRoleDto)
        {
            var userRole = _mapper.Map<UserRole>(createUserRoleDto);

            await _repository.AddAsync(userRole);

            var userRoleDto = _mapper.Map<UserRoleDto>(userRole);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = userRole.Id }, userRoleDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromQuery] Guid id, CreateUserRoleDto createUserRoleDto)
        {
            var userRole = await _repository.GetByIdAsync(id);

            if (userRole == null)
            {
                return NotFound();
            }

            _mapper.Map(createUserRoleDto, userRole);

            await _repository.UpdateAsync(userRole);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var userRole = await _repository.GetByIdAsync(id);

            if (userRole == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(userRole);

            return NoContent();
        }
    }





}
