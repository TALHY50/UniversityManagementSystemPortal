using AutoMapper;
using Microsoft.AspNetCore.Http;
using PorgramNamespace = UniversityManagementsystem.Models.Program;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystemPortal.ModelDto.Program;
using UniversityManagementSystemPortal.ModelDto.StudentProgram;
using UniversityManagementSystemPortal.Authorization.UniversityManagementSystemPortal.Authorization;
using Microsoft.AspNetCore.Authorization;
using UniversityManagementSystemPortal.IdentityServices;

namespace UniversityManagementSystemPortal.Controllers
{
    //[JwtAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramsController : ControllerBase
    {
        private readonly IIdentityServices _identityService;
        private readonly IMapper _mapper;
        private readonly IProgramRepository _repository;

        public ProgramsController(IProgramRepository repository, IMapper mapper, IIdentityServices identityService)
        {
            _repository = repository;
            _mapper = mapper;
            _identityService = identityService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramReadDto>>> GetAll()
        {
            var programs = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProgramReadDto>>(programs));
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProgramReadDto>> GetById(Guid id)
        {
            var program = await _repository.GetByIdAsync(id);
            if (program == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProgramReadDto>(program));
        }
        //[JwtAuthorize("Admin, SuperAdmin")]
        [HttpPost]
        public async Task<ActionResult<ProgramReadDto>> Create(ProgramCreateDto programCreateDto)
        {
            var program = _mapper.Map<PorgramNamespace>(programCreateDto);
            await _repository.AddAsync(program);

            var programReadDto = _mapper.Map<ProgramReadDto>(program);
            return CreatedAtRoute(nameof(GetById), new { id = programReadDto.Id }, programReadDto);
        }
        [JwtAuthorize("Admin, SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, ProgramUpdateDto programUpdateDto)
        {
            var program = await _repository.GetByIdAsync(id);
            if (program == null)
            {
                return NotFound("Program not found.");
            }

            var userId = _identityService.GetUserId();
            if (userId != null)
            {
                program.UpdatedBy = userId.Value;
            }

            _mapper.Map(programUpdateDto, program);
            await _repository.UpdateAsync(program);

            return NoContent();
        }
        [JwtAuthorize("Admin, SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var program = await _repository.GetByIdAsync(id);
            if (program == null)
            {
                return NotFound("Program not found.");
            }

            await _repository.DeleteAsync(id);
            return Ok("Program deleted successfully.");
        }

        [JwtAuthorize("Admin, SuperAdmin")]
        [HttpGet("{id}/studentprograms")]
        public async Task<ActionResult<IEnumerable<StudentProgramReadDto>>> GetStudentPrograms(Guid id)
        {
            var studentPrograms = await _repository.GetStudentProgramsAsync(id);
            return Ok(_mapper.Map<IEnumerable<StudentProgramReadDto>>(studentPrograms));
        }
    }
}
