using AutoMapper;
using Microsoft.AspNetCore.Http;
using PorgramNamespace = UniversityManagementsystem.Models.Program;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystemPortal.ModelDto.Program;
using UniversityManagementSystemPortal.ModelDto.StudentProgram;

namespace UniversityManagementSystemPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProgramRepository _repository;

        public ProgramsController(IProgramRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramReadDto>>> GetAll()
        {
            var programs = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProgramReadDto>>(programs));
        }

        [HttpGet("{id}", Name = "GetProgramById")]
        public async Task<ActionResult<ProgramReadDto>> GetById(Guid id)
        {
            var program = await _repository.GetByIdAsync(id);
            if (program == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProgramReadDto>(program));
        }

        [HttpPost]
        public async Task<ActionResult<ProgramReadDto>> Create(ProgramCreateDto programCreateDto)
        {
            var program = _mapper.Map<PorgramNamespace>(programCreateDto);
            await _repository.AddAsync(program);

            var programReadDto = _mapper.Map<ProgramReadDto>(program);
            return/* CreatedAtRoute/*(nameof(GetById), new { id = programReadDto.Id },*/ programReadDto;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, ProgramUpdateDto programUpdateDto)
        {
            var program = await _repository.GetByIdAsync(id);
            if (program == null)
            {
                return NotFound();
            }

            _mapper.Map(programUpdateDto, program);
            await _repository.UpdateAsync(program);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var program = await _repository.GetByIdAsync(id);
            if (program == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}/studentprograms")]
        public async Task<ActionResult<IEnumerable<StudentProgramReadDto>>> GetStudentPrograms(Guid id)
        {
            var studentPrograms = await _repository.GetStudentProgramsAsync(id);
            return Ok(_mapper.Map<IEnumerable<StudentProgramReadDto>>(studentPrograms));
        }
    }
}
