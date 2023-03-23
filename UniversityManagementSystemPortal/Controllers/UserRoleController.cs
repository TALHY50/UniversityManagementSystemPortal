using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Application.Command.UserRoles;
using UniversityManagementSystemPortal.Application.Qurey.UserRoles;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Authorization.UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Interfce;
using UniversityManagementSystemPortal.ModelDto.UserRoleDto;

namespace UniversityManagementSystemPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRolesController : ControllerBase
    {
        private readonly IMediator _mediator;
       

        public UserRolesController(IMediator mediator)
        {
            
            _mediator = mediator;
           
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserRoleDto>> GetById(Guid id)
        {
            var query = new GetUserRoleByIdQuery { Id = id };

            var userRoleDto = await _mediator.Send(query);

            return userRoleDto;
        }
        [JwtAuthorize("Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRoleDto>>> GetAll()
        {
            var query = new GetAllUserRolesQuery();

            var userRoleDtos = await _mediator.Send(query);

            return Ok(userRoleDtos);
        }

        [HttpPost]
        public async Task<ActionResult<UserRoleDto>> Create([FromBody] CreateUserRoleDto createUserRoleDto)
        {
            var command = new CreateUserCommand { CreateUserRoleDto = createUserRoleDto };

            var userRoleDto = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = userRoleDto.Id }, userRoleDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromQuery] Guid id, CreateUserRoleDto createUserRoleDto)
        {
            var command = new UpdateUserRoleCommand { Id = id, UserRoleUpdateDto = createUserRoleDto };

            await _mediator.Send(command);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteUserRoleCommand { Id = id };

            await _mediator.Send(command);

            return NoContent();
        }
    }

}
