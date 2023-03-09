using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Institute;
using UniversityManagementSystemPortal.ModelDto.InstituteDto;

namespace UniversityManagementSystemPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IInstituteRepository _repository;

        public InstitutesController(IMapper mapper, IInstituteRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
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
        public async Task<ActionResult> Create(InstituteCreateDto instituteCreateDto, InstituteType institutetype)
        {
            if (instituteCreateDto == null)
            {
                return BadRequest();
            }

            var institute = _mapper.Map<Institute>(instituteCreateDto);
            institute.IsAutoIncrementAdmissionNo = instituteCreateDto.IsAutoIncrementAdmissionNo; // Set the property value

            await _repository.AddAsync(institute);

            return CreatedAtAction(nameof(GetById), new { id = institute.Id }, institute);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, InstituteUpdateDto instituteUpdateDto)
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
            institute.IsAutoIncrementAdmissionNo = instituteUpdateDto.IsAutoIncrementAdmissionNo; // Set the property value

            await _repository.UpdateAsync(institute);

            return NoContent();
        }

    }

}
