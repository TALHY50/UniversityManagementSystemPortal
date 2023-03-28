using LINQtoCSV;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystemPortal.Application.Command.Employee;
using UniversityManagementSystemPortal.Application.Qurey.Employee;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Helpers.Paging;
using UniversityManagementSystemPortal.ModelDto.Employee;

namespace UniversityManagementSystemPortal.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(ILogger<EmployeeController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;

        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<EmployeeDto>>> Get([FromQuery] PaginatedViewModel PaginatedViewModel)
        {
            var employees = await _mediator.Send(new GetAllEmployeesQuery { paginatedViewModel = PaginatedViewModel });

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetById(Guid id)
        {
            try
            {
                var query = new GetEmployeeByIdQuery { Id = id };
                var employee = await _mediator.Send(query);
                return Ok(employee);
            }
            catch (AppException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] CreateEmployeeCommand createEmployeeCommand)
        {
            try
            {
                var createdEmployee = await _mediator.Send(createEmployeeCommand);
                return Ok(new { message = "Employee created successfully.", employee = createdEmployee });
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating an employee");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while creating an employee." });
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateEmployeeDto>> Update(Guid id, [FromForm] UpdateEmployeeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("The provided ID does not match the ID in the request body.");
            }

            try
            {
                var updatedEmployee = await _mediator.Send(command);
                return Ok(new { message = "Employee updated successfully", employee = updatedEmployee });
            }
            catch (AppException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteEmployeeCommand { Id = id });

            return NoContent();
        }

        [HttpGet("Export")]
        public async Task<IActionResult> Export()
        {
            var request = new ExportEmployeeListQuery();
            var fileContent = await _mediator.Send(request);
            var fileName = "employee_list.csv";
            return File(fileContent, "text/csv", fileName);
        }
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
                var csvContext = new CsvContext();
                var employeeData = csvContext.Read<EmployeeReadModel>(streamReader, csvFileDescription).ToList();

                var command = new ImportEmployeeCommand(employeeData);
                var skippedEntries =   await _mediator.Send(command);
                if (skippedEntries.Any())
                {
                    return BadRequest(new { message = "File Imported with some skipped entries", skippedEntries });
                }
            }

            _logger.LogInformation("Processing file {FileName} for user {UserId}.", file.FileName);

            return Ok(new { message = "File Imported Successfully" });
        }

    }




}
