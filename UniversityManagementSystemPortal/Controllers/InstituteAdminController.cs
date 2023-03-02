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
        private readonly IInstituteAdminRepository _instituteAdminRepository;
        private readonly IMapper _mapper;

        public InstituteAdminController(IInstituteAdminRepository instituteAdminRepository, IMapper mapper)
        {
            _instituteAdminRepository = instituteAdminRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InstituteAdminDTO>> GetByIdAsync(Guid id)
        {
            var instituteAdmin = await _instituteAdminRepository.GetByIdAsync(id);

            if (instituteAdmin == null)
            {
                return NotFound();
            }

            var instituteAdminDto = _mapper.Map<InstituteAdminDTO>(instituteAdmin);
            return Ok(instituteAdminDto);
        }

        [HttpGet("byuser/{userId}")]
        public async Task<ActionResult<InstituteAdminDTO>> GetByUserIdAsync(Guid userId)
        {
            var instituteAdmin = await _instituteAdminRepository.GetByUserIdAsync(userId);

            if (instituteAdmin == null)
            {
                return NotFound();
            }

            var instituteAdminDto = _mapper.Map<InstituteAdminDTO>(instituteAdmin);
            return Ok(instituteAdminDto);
        }

        [HttpGet("{instituteId}/admins")]
        public async Task<ActionResult<IEnumerable<InstituteAdminDTO>>> GetInstituteAdminsAsync(Guid instituteId)
        {
            var instituteAdmins = await _instituteAdminRepository.GetInstituteAdminsAsync(instituteId);

            var instituteAdminDtos = _mapper.Map<IEnumerable<InstituteAdminDTO>>(instituteAdmins);
            return Ok(instituteAdminDtos);
        }

        [HttpPost]
        public async Task<ActionResult<InstituteAdminDTO>> AddAsync(InstituteAdminDTO instituteAdminDto)
        {
            var instituteAdmin = _mapper.Map<InstituteAdmin>(instituteAdminDto);
            await _instituteAdminRepository.AddAsync(instituteAdmin);

            var insertedInstituteAdminDto = _mapper.Map<InstituteAdminDTO>(instituteAdmin);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = insertedInstituteAdminDto.Id }, insertedInstituteAdminDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, InstituteAdminDTO instituteAdminDto)
        {
            if (id != instituteAdminDto.Id)
            {
                return BadRequest();
            }

            var instituteAdmin = await _instituteAdminRepository.GetByIdAsync(id);

            if (instituteAdmin == null)
            {
                return NotFound();
            }

            _mapper.Map(instituteAdminDto, instituteAdmin);
            await _instituteAdminRepository.UpdateAsync(instituteAdmin);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var instituteAdmin = await _instituteAdminRepository.GetByIdAsync(id);

            if (instituteAdmin == null)
            {
                return NotFound();
            }

            await _instituteAdminRepository.DeleteAsync(instituteAdmin);
            return NoContent();
        }
    }



}
