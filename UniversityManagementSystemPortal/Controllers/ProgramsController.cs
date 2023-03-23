using AutoMapper;
using Microsoft.AspNetCore.Http;
using PorgramNamespace = UniversityManagementsystem.Models.Program;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystemPortal.ModelDto.Program;
using UniversityManagementSystemPortal.ModelDto.StudentProgram;
using UniversityManagementSystemPortal.Authorization.UniversityManagementSystemPortal.Authorization;
using Microsoft.AspNetCore.Authorization;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Application.Qurey.Program;
using MediatR;
using UniversityManagementSystemPortal.Application.Command.Program;

namespace UniversityManagementSystemPortal.Controllers
{
    //[JwtAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramsController : ControllerBase
    {
        private readonly IIdentityServices _identityService;
        private readonly IMapper _mapper;
        private readonly IProgramRepository _repository;
        private readonly IMediator _mediator;
        public ProgramsController(IMediator mediator, IProgramRepository repository, IMapper mapper, IIdentityServices identityService)
        {
            _mediator = mediator;
            _repository = repository;
            _mapper = mapper;
            _identityService = identityService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramReadDto>>> GetAll()
        {
            var programs = await _mediator.Send(new GetAllProgramsQuery());
            return Ok(programs);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProgramReadDto>> GetById(Guid id)
        {
            var query = new GetProgramByIdQuery { ProgramId = id };
            var program = await _mediator.Send(query);

            return Ok(program);
        }
        //[JwtAuthorize("Admin, SuperAdmin")]
        [HttpPost]
        public async Task<ActionResult<ProgramReadDto>> Create(ProgramCreateDto programCreateDto)
        {
            var command = _mapper.Map<CreateProgramCommand>(programCreateDto);
            var program = await _mediator.Send(command);

            return CreatedAtRoute(nameof(GetById), new { id = program.Id }, program);
        }
        [JwtAuthorize("Admin, SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, ProgramUpdateDto programUpdateDto)
        {
            var command = _mapper.Map<UpdateProgramCommand>(programUpdateDto);
            command.Id = id;
            await _mediator.Send(command);

            return NoContent();
        }
        [JwtAuthorize("Admin, SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteProgramCommand { Id = id });
            return Ok("Program deleted successfully.");
        }

        [JwtAuthorize("Admin, SuperAdmin")]
        [HttpGet("{id}/studentprograms")]
        public async Task<ActionResult<IEnumerable<StudentProgramReadDto>>> GetStudentPrograms(Guid id)
        {
            var studentPrograms = await _repository.GetStudentProgramsAsync(id);
            return Ok(_mapper.Map<IEnumerable<StudentProgramReadDto>>(studentPrograms));
        }
    }
}
