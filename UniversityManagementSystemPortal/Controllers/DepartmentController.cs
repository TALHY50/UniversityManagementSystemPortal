using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Application.Command.Department;
using UniversityManagementSystemPortal.Application.Qurey.Department;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Department;

namespace UniversityManagementSystemPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IIdentityServices _identityService;

        public DepartmentController(IDepartmentRepository departmentRepository, IMediator mediator, IMapper mapper, IIdentityServices identityService)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _identityService = identityService;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAll()
        {
            var query = new GetAllDepartmentsQuery();
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

        [HttpGet("institute/{id}")]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetByInstituteId(Guid id)
        {
            var query = new GetDepartmentsByInstituteIdQuery { InstituteId = id };
            var departmentDtos = await _mediator.Send(query);
            return Ok(departmentDtos);
        }
        [HttpPost("departments")]

        public async Task<ActionResult<DepartmentDto>> Create(DepartmentCreateDto departmentCreateDto)
        {
            var department = _mapper.Map<Department>(departmentCreateDto);

            department.CreatedBy = _identityService.GetUserId();
            department.UpdatedBy = _identityService.GetUserId();

            department = await _departmentRepository.CreateDepartmentAsync(department);

            var departmentDto = _mapper.Map<DepartmentDto>(department);
            return departmentDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, DepartmentUpdateDto updateDepartmentDto)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            department.UpdatedBy = _identityService.GetUserId();

            _mapper.Map(updateDepartmentDto, department);

            await _departmentRepository.UpdateDepartmentAsync(department);
            return NoContent();
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
