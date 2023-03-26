using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystemPortal.Application.Command.Roles;
using UniversityManagementSystemPortal.Application.Qurey.Roles;
using UniversityManagementSystemPortal.Authorization.UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.ModelDto.Role;
using UniversityManagementSystemPortal.Models.ModelDto.Role;

namespace UniversityManagementSystemPortal.Controllers
{
    [JwtAuthorize("Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> Get()
        {
            var roles = await _mediator.Send(new GetAllRolesQuery());
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetById(Guid id)
        {
            var role = await _mediator.Send(new GetRoleByIdQuery { Id = id });

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<RoleDto>> Create(AddRoleDto createRoleDto)
        {
            var command = new AddRoleCommand { Name = createRoleDto.Name, RoleType = (RoleType)createRoleDto.RoleType };

            var role = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), role);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateRoleDto updateRoleDto)
        {
            if (id != updateRoleDto.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(new UpdateRoleCommand { Id = id, Name = updateRoleDto.Name, RoleType = updateRoleDto.RoleType });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteRoleCommand { Id = id });
            return NoContent();
        }

        //[HttpGet("byRoleType/{roleType}")]
        //public async Task<ActionResult<RoleDto>> GetByRoleType(int roleType)
        //{
        //    var role = await _mediator.Send(new GetRoleByRoleTypeQuery { RoleType = roleType });

        //    if (role == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(role);
        //}
    }


}
