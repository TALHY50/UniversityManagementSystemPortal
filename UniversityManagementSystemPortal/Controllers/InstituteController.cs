using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Authorization.UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Institute;
using UniversityManagementSystemPortal.ModelDto.InstituteDto;

namespace UniversityManagementSystemPortal.Controllers
{
    [JwtAuthorize("Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IInstituteRepository _repository;
        private readonly IIdentityServices _identityServices;

        public InstitutesController(IMapper mapper, IInstituteRepository repository, IIdentityServices identityServices)
        {
            _mapper = mapper;
            _repository = repository;
            _identityServices = identityServices;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InstituteDto>> GetById(Guid id)
        {
            var institute = await _repository.GetByIdAsync(id);

            if (institute == null)
            {
                return NotFound();
            }

            var instituteDto = _mapper.Map<InstituteDto>(institute);

            return instituteDto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstituteDto>>> GetAll()
        {
            var institutes = await _repository.GetAllAsync();

            var institutesDto = _mapper.Map<IEnumerable<InstituteDto>>(institutes);

            return Ok(institutesDto);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] InstituteCreateDto instituteCreateDto, InstituteType institutetype)
        {
            if (instituteCreateDto == null)
            {
                return BadRequest();
            }

            var institute = _mapper.Map<Institute>(instituteCreateDto);
            institute.IsAutoIncrementAdmissionNo = instituteCreateDto.IsAutoIncrementAdmissionNo; 

            institute.CreatedBy = _identityServices.GetUserId(); 
            institute.UpdatedBy = _identityServices.GetUserId();
            await _repository.AddAsync(institute);

            return CreatedAtAction(nameof(GetById), new { id = institute.Id }, new { message = "Institute created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromForm] Guid id, InstituteUpdateDto instituteUpdateDto)
        {
            if (instituteUpdateDto == null || id != instituteUpdateDto.Id)
            {
                return BadRequest();
            }

            var institute = await _repository.GetByIdAsync(id);

            if (institute == null)
            {
                return NotFound();
            }

            _mapper.Map(instituteUpdateDto, institute);
            institute.IsAutoIncrementAdmissionNo = instituteUpdateDto.IsAutoIncrementAdmissionNo;

            institute.UpdatedBy = _identityServices.GetUserId(); 

            await _repository.UpdateAsync(institute);

            return Ok(new { message = "Institute updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var institute = await _repository.GetByIdAsync(id);

            if (institute == null)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(id);

            return Ok("Institute deleted successfully.");
        }

    }


}
