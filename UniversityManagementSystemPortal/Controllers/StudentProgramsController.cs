using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.StudentProgram;

namespace UniversityManagementSystemPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentProgramsController : ControllerBase
    {
        private readonly IStudentProgramRepository _studentProgramRepository;
        private readonly IMapper _mapper;

        public StudentProgramsController(IStudentProgramRepository studentProgramRepository, IMapper mapper)
        {
            _studentProgramRepository = studentProgramRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentProgramDto>>> GetAllStudentProgramsAsync()
        {
            var studentPrograms = await _studentProgramRepository.GetAllStudentProgramsAsync();
            var studentProgramDtos = _mapper.Map<IEnumerable<StudentProgramDto>>(studentPrograms);
            return Ok(studentProgramDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentProgramDto>> GetStudentProgramByIdAsync(Guid id)
        {
            var studentProgram = await _studentProgramRepository.GetStudentProgramByIdAsync(id);
            if (studentProgram == null)
            {
                return NotFound();
            }
            var studentProgramDto = _mapper.Map<StudentProgramDto>(studentProgram);
            return Ok(studentProgramDto);
        }

        [HttpPost]
        public async Task<ActionResult<StudentProgramDto>> AddStudentProgramAsync(StudentProgramCreateDto studentProgramCreateDto)
        {
            var studentProgram = _mapper.Map<StudentProgram>(studentProgramCreateDto);
            await _studentProgramRepository.AddStudentProgramAsync(studentProgram);
            var studentProgramDto = _mapper.Map<StudentProgramDto>(studentProgram);
            return CreatedAtAction(nameof(GetStudentProgramByIdAsync), new { id = studentProgramDto.Id }, studentProgramDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudentProgramAsync(Guid id, StudentProgramUpdateDto studentProgramUpdateDto)
        {
            var studentProgram = await _studentProgramRepository.GetStudentProgramByIdAsync(id);
            if (studentProgram == null)
            {
                return NotFound();
            }
            _mapper.Map(studentProgramUpdateDto, studentProgram);
            await _studentProgramRepository.UpdateStudentProgramAsync(studentProgram);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentProgramAsync(Guid id)
        {
            var studentProgram = await _studentProgramRepository.GetStudentProgramByIdAsync(id);
            if (studentProgram == null)
            {
                return NotFound();
            }
            await _studentProgramRepository.DeleteStudentProgramAsync(id);
            return NoContent();
        }
    }
}
