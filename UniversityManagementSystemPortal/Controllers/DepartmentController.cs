using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Department;

namespace UniversityManagementSystemPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IIdentityServices _identityService;

        public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper, IIdentityServices identityService)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAll()
        {
            var departments = await _departmentRepository.GetAllDepartmentsAsync();
            var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return Ok(departmentDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetById(Guid id)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            var departmentDto = _mapper.Map<DepartmentDto>(department);
            return Ok(departmentDto);
        }

        [HttpGet("institute/{id}")]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetByInstituteId(Guid id)
        {
            var departments = await _departmentRepository.GetDepartmentsByInstituteIdAsync(id);
            var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
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
            var department = await _departmentRepository.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            await _departmentRepository.DeleteDepartmentAsync(id);
            return NoContent();
        }
    }

}
