
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystemPortal.Application.Command.Position;
using UniversityManagementSystemPortal.Application.Handler.Postion;
using UniversityManagementSystemPortal.Application.Qurey.Position;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Authorization.UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Helpers.Paging;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Position;
using UniversityManagementSystemPortal.Models.ModelDto.Program;

namespace UniversityManagementSystemPortal.Controllers
{


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
            public async Task<ActionResult<PaginatedList<PositionDto>>> GetAll(PaginatedViewModel paginatedViewModel)
            {
                var positions = await _mediator.Send(new GetAllPositionsQuery { paginatedViewModel = paginatedViewModel });

                return Ok(positions);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<PositionDto>> GetById(Guid id)
            {
                var query = new GetPositionByIdQuery { Id = id };
                var positions = await _mediator.Send(query);

                if (positions == null || positions.Count() == 0)
                {
                    return NotFound();
                }

                return Ok(positions.First());
            }
            [HttpGet("Lookup")]
            public async Task<IActionResult> GetPositions()
            {
                var positions = await _mediator.Send(new GetLookupPositionList(null));

                if (positions == null || positions.Count == 0)
                {
                    return NotFound();
                }

                return Ok(positions);
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
