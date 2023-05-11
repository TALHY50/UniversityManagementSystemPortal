using AutoMapper;
using LINQtoCSV;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystemPortal.Application.Command.Student;
using UniversityManagementSystemPortal.Application.Qurey.Student;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Authorization.UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Helpers.Paging;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Models.ModelDto.Student;
using UniversityManagementSystemPortal.PictureManager;

namespace UniversityManagementSystemPortal.Controllers
{
    [JwtAuthorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<StudentController> _logger;
        public StudentController(ILogger<StudentController> logger,IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [JwtAuthorize("Students", "Admin", "SuperAdmin", "Teacher")]
        [HttpGet]
        public async Task<ActionResult<PaginatedList<StudentDto>>> Get([FromQuery] PaginatedViewModel PaginatedViewModel)
        {
            var query = new GetStudentListQuery{ paginatedViewModel = PaginatedViewModel };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [JwtAuthorize("Students", "Admin", "SuperAdmin", "Teacher")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetStudentByIdQurey(id);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
        [JwtAuthorize("Admin", "SuperAdmin")]
        [HttpGet("export")]
        public async Task<IActionResult> ExportStudents()
        {
            var query = new ExportStudentListQuery();
            var data = await _mediator.Send(query);

            return File(data, "text/csv", "students.csv");
        }
        [JwtAuthorize("Admin", "SuperAdmin")]
        [HttpPost("Import")]
        public async Task<IActionResult> UploadFileAndProcessData(IFormFile file)
        {
            var csvFileDescription = new CsvFileDescription
            {
                FirstLineHasColumnNames = true,
                IgnoreUnknownColumns = true,
                SeparatorChar = ',',
                UseFieldIndexForReadingData = false
            };

            using (var streamReader = new StreamReader(file.OpenReadStream()))
            {
                var csvContext = new LINQtoCSV.CsvContext();
                var studentsData = csvContext.Read<StudentReadModel>(streamReader, csvFileDescription).ToList();

                var command = new ImportStudentsCommand(studentsData);
               var skippedEntries =  await _mediator.Send(command);
                if (skippedEntries.Any())
                {
                    // Return the list of skipped entries as a response
                    return BadRequest(new { message = "File Imported with some skipped entries", skippedEntries });
                }
            }

            _logger.LogInformation("Processing file {FileName} for user {UserId}.", file.FileName);

            return Ok(new { message = "File Imported Successfully" });
        }


        [HttpPost]
        public async Task<ActionResult<AddStudentDto>> AddStudent([FromForm] AddStudentDto command)
        {
            try
            {
                var result = await _mediator.Send(new AddStudentCommand(command));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{admissionNo}")]
        public async Task<IActionResult> GetByAdmissionNo(string admissionNo)
        {
            var query = new GetStudentByAdmissionNoQuery { AdmissionNo = admissionNo };
            var studentDto = await _mediator.Send(query);
            return Ok(studentDto);
        }
        [JwtAuthorize("Admin", "SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(Guid id, [FromForm] UpdateStudentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("The ID in the URL doesn't match the ID in the request body.");
            }

            var result = await _mediator.Send(command);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        [JwtAuthorize("Admin", "SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteStudentCommand { Id = id });
                return Ok(new { message = "Student deleted successfully." });
            }
            catch (AppException ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting student with Id {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Internal server error" });
            }
        }



    }
}

