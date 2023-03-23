using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UniversityManagementSystemPortal.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using global::AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using UniversityManagementsystem.Models;
    using UniversityManagementSystemPortal.Application.Command.Position;
    using UniversityManagementSystemPortal.Application.Qurey.Position;
    using UniversityManagementSystemPortal.Authorization;
    using UniversityManagementSystemPortal.Authorization.UniversityManagementSystemPortal.Authorization;
    using UniversityManagementSystemPortal.Interfaces;
    using UniversityManagementSystemPortal.ModelDto.Position;
  

    namespace YourApplicationName.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class PositionController : ControllerBase
        {
            private readonly IMediator _mediator;
            private readonly IPositionRepository _repository;
            private readonly IMapper _mapper;

            public PositionController(IPositionRepository repository, IMapper mapper, IMediator mediator)
            {
                _repository = repository;
                _mapper = mapper;
                _mediator = mediator;
            }
            [JwtAuthorize("Admin", "SuperAdmin")]
            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var positions = await _mediator.Send(new GetAllPositionsQuery());

                return Ok(positions);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<PositionDto>> GetById(Guid id)
            {
                var query = new GetPositionByIdQuery { Id = id};
                var positions = await _mediator.Send(query);

                if (positions == null || positions.Count() == 0)
                {
                    return NotFound();
                }

                return Ok(positions.First());
            }

            [HttpPost]
            public async Task<ActionResult<PositionDto>> Create(PositionAddorUpdateDto positionAddDto)
            {
                var command = _mapper.Map<CreatePositionCommand>(positionAddDto);
                var positionDto = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetById), new { id = positionDto.Id }, positionDto);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(Guid id, [FromForm] PositionAddorUpdateDto positionUpdateDto)
            {
                var updatePositionCommand = new UpdatePositionCommand(id, positionUpdateDto);

                try
                {
                    await _mediator.Send(updatePositionCommand);
                }
                catch (AppException ex)
                {
                    return BadRequest(ex.Message);
                }

                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(Guid id)
            {
                var result = await _mediator.Send(new DeletePositionCommand { Id = id });

                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }

}
