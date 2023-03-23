using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystemPortal.Application.Command.StudentProgram;
using UniversityManagementSystemPortal.Application.Qurey.StudentProgram;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.ModelDto.StudentProgram;

namespace UniversityManagementSystemPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentProgramsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<StudentProgramsController> _logger;

        public StudentProgramsController(ILogger<StudentProgramsController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentProgramDto>>> GetAllStudentProgramsAsync()
        {
            var query = new GetAllStudentProgramsQuery();

            var studentProgramDtos = await _mediator.Send(query);

            return Ok(studentProgramDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentProgramDto>> GetStudentProgramByIdAsync(Guid id)
        {
            var query = new GetStudentProgramByIdQuery { Id = id };

            var studentProgramDto = await _mediator.Send(query);

            if (studentProgramDto == null)
            {
                return NotFound();
            }

            return Ok(studentProgramDto);
        }

        [HttpPost]
        public async Task<ActionResult<StudentProgramDto>> AddStudentProgramAsync(StudentProgramCreateDto studentProgramCreateDto)
        {
            var command = new AddStudentProgramCommand { StudentProgramCreateDto = studentProgramCreateDto };

            var studentProgramDto = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetStudentProgramByIdAsync), new { id = studentProgramDto.Id }, studentProgramDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudentProgramAsync(Guid id, [FromForm] StudentProgramUpdateDto studentProgramUpdateDto)
        {
            _logger.LogInformation("Updating student program with ID {Id}", id);

            var command = new UpdateStudentProgramCommand(id, studentProgramUpdateDto);

            try
            {
                await _mediator.Send(command);

                _logger.LogInformation("Student program with ID {Id} updated successfully", id);

                return NoContent();
            }
            catch (AppException ex)
            {
                _logger.LogWarning("Student program with ID {Id} not found", id);

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating student program with ID {Id}", id);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentProgramAsync(Guid id)
        {
            await _mediator.Send(new DeleteStudentProgramCommand { Id = id });
            return NoContent();
        }
    }
}
