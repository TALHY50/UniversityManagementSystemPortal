using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using UniversityManagementSystemPortal.Application.Command.Institute;
using UniversityManagementSystemPortal.Application.Qurey.Institute;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Authorization.UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Institute;
using UniversityManagementSystemPortal.ModelDto.InstituteDto;
using UniversityManagementSystemPortal.Models.ModelDto.Institute;

namespace UniversityManagementSystemPortal.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutesController : ControllerBase
    {
        private readonly ILogger<InstitutesController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IInstituteRepository _repository;
        private readonly IIdentityServices _identityServices;

        public InstitutesController(ILogger<InstitutesController> logger, IMediator mediator, IMapper mapper, IInstituteRepository repository, IIdentityServices identityServices)
        {
            _mediator = mediator;
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _identityServices = identityServices;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<InstituteDto>> GetById(Guid id)
        {
            var query = new GetInstituteByIdQuery { Id = id };
            var instituteDto = await _mediator.Send(query);

            if (instituteDto == null)
            {
                return NotFound();
            }

            return instituteDto;
        }

        [HttpGet]
        public async Task<ActionResult<List<InstituteDto>>> GetAll()
        {
            var query = new GetAllInstitutesQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create( InstituteCreateDto instituteCreateDto)
        {
            if (instituteCreateDto == null)
            {
                return BadRequest();
            }

            var institute = _mapper.Map<Institute>(instituteCreateDto);
            institute.IsAutoIncrementAdmissionNo = instituteCreateDto.IsAutoIncrementAdmissionNo;

            institute.CreatedBy = _identityServices.GetUserId();
            institute.UpdatedBy = _identityServices.GetUserId();
            await _repository.AddAsync(institute);

            return CreatedAtAction(nameof(GetById), new { id = institute.Id }, new { message = "Institute created successfully" });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id,InstituteUpdateDto instituteUpdateDto)
        {
          

            var command = _mapper.Map<InstituteUpdateCommand>(instituteUpdateDto);
            command.Id = id;

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteInstituteCommand { Id = id });

                return Ok("Institute deleted successfully.");
            }
            catch (AppException ex)
            {
                _logger.LogWarning("Failed to delete institute: {ErrorMessage}", ex.Message);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting institute");
                return StatusCode(500, "An error occurred while deleting institute");
            }
        }


    }


}
