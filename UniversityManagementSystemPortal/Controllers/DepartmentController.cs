using MediatR;
using Microsoft.AspNetCore.Mvc;

using UniversityManagementSystemPortal.Application.Command.Department;
using UniversityManagementSystemPortal.Application.Qurey.Department;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Helpers.Paging;
using UniversityManagementSystemPortal.ModelDto.Department;
using UniversityManagementSystemPortal.Models.ModelDto.Department;

namespace UniversityManagementSystemPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {

            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<PaginatedList<DepartmentDto>>> GetAll(PaginatedViewModel paginatedViewModel)
        {
            var query = new GetAllDepartmentsQuery{paginatedViewModel = paginatedViewModel};
            var departmentDtos = await _mediator.Send(query);
            return Ok(departmentDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetById(Guid id)
        {
            var query = new GetDepartmentByIdQuery { Id = id };
            var departmentDto = await _mediator.Send(query);
            if (departmentDto == null)
            {
                return NotFound();
            }
            return Ok(departmentDto);
        }
        [HttpGet("lookup")]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _mediator.Send(new GetLookupList(null));

            if (departments == null || departments.Count == 0)
            {
                return NotFound();
            }

            return Ok(departments);
        }
        [HttpGet("institute/{id}")]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetByInstituteId(Guid id)
        {
            var query = new GetDepartmentsByInstituteIdQuery { InstituteId = id };
            var departmentDtos = await _mediator.Send(query);
            return Ok(departmentDtos);
        }
        [HttpPost]
        public async Task<ActionResult<DepartmentCreateDto>> Create([FromForm]DepartmentCreateDto command)
        {
            try
            {
                var result = await _mediator.Send(new CreateDepartmentCommand (command));
                return Ok(result);
            }
            catch (AppException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while creating the department" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DepartmentUpdateDto>> UpdateDepartment(Guid id, [FromBody] UpdateDepartmentCommand request)
        {
            try
            {
                var command = new UpdateDepartmentCommand { Id = id, Name = request.Name, Code = request.Code };
                var departmentDto = await _mediator.Send(command);

                return Ok(departmentDto);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // log the exception message here
                return StatusCode(500, "An error occurred while updating the department.");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteDepartmentCommand { DepartmentId = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }

}
