using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.InstituteAdmin;

namespace UniversityManagementSystemPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituteAdminController : ControllerBase
    {
        private readonly IInstituteAdminRepository _repository;
        private readonly IMapper _mapper;

        public InstituteAdminController(IInstituteAdminRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InstituteAdminDTO>> GetById(Guid id)
        {
            var instituteAdmin = await _repository.GetByIdAsync(id);

            if (instituteAdmin == null)
            {
                return NotFound();
            }

            var instituteAdminDto = _mapper.Map<InstituteAdminDTO>(instituteAdmin);

            return Ok(instituteAdminDto);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<InstituteAdminDTO>> GetByUserId(Guid userId)
        {
            var instituteAdmin = await _repository.GetByUserIdAsync(userId);

            if (instituteAdmin == null)
            {
                return NotFound();
            }

            var instituteAdminDto = _mapper.Map<InstituteAdminDTO>(instituteAdmin);

            return Ok(instituteAdminDto);
        }

        [HttpPost]
        public async Task<ActionResult> Create(InstituteAdminCreateDto instituteAdminCreateDto)
        {
            var instituteAdmin = _mapper.Map<InstituteAdmin>(instituteAdminCreateDto);

            await _repository.AddAsync(instituteAdmin);

            return CreatedAtAction(nameof(GetById), new { id = instituteAdmin.Id }, null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, InstituteAdminUpdateDto instituteAdminUpdateDto)
        {
            if (id != instituteAdminUpdateDto.Id)
            {
                return BadRequest();
            }

            var instituteAdmin = await _repository.GetByIdAsync(id);

            if (instituteAdmin == null)
            {
                return NotFound();
            }

            _mapper.Map(instituteAdminUpdateDto, instituteAdmin);

            await _repository.UpdateAsync(instituteAdmin);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var instituteAdmin = await _repository.GetByIdAsync(id);

            if (instituteAdmin == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(instituteAdmin);

            return NoContent();
        }

        [HttpGet("institute/{instituteId}")]
        public async Task<ActionResult<IEnumerable<InstituteAdminDTO>>> GetInstituteAdmins(Guid instituteId)
        {
            var instituteAdmins = await _repository.GetInstituteAdminsAsync(instituteId);

            if (instituteAdmins == null)
            {
                return NotFound();
            }

            var instituteAdminsDto = _mapper.Map<IEnumerable<InstituteAdminDTO>>(instituteAdmins);

            return Ok(instituteAdminsDto);
        }

        [HttpGet("user/{userId}/is-super-admin")]
        public async Task<ActionResult<bool>> IsSuperAdmin(Guid userId)
        {
            var isSuperAdmin = await _repository.IsSuperAdminAsync(userId);

            if (!isSuperAdmin)
            {
                return NotFound("User is not a super admin.");
            }

            return Ok(isSuperAdmin);
        }

        [HttpGet("user/{userId}/is-admin/{instituteId}")]
        public async Task<ActionResult<bool>> IsAdmin(Guid userId, Guid instituteId)
        {
            var isAdmin = await _repository.IsAdminAsync(userId, instituteId);

            if (!isAdmin)
            {
                return NotFound("User is not an admin for the specified institute.");
            }

            return Ok(isAdmin);
        }
    }

}

