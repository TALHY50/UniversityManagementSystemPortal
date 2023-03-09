using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementsystem.Models;
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

        public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAllDepartmentsAsync()
        {
            var departments = await _departmentRepository.GetAllDepartmentsAsync();
            var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return Ok(departmentDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetDepartmentByIdAsync(Guid id)
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
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartmentsByInstituteIdAsync(Guid id)
        {
            var departments = await _departmentRepository.GetDepartmentsByInstituteIdAsync(id);
            var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return Ok(departmentDtos);
        }

        [HttpPost("departments")]
        public async Task<ActionResult<DepartmentDto>> CreateDepartmentAsync(DepartmentCreateDto departmentCreateDto)
        {
            var department = _mapper.Map<Department>(departmentCreateDto);
            department = await _departmentRepository.CreateDepartmentAsync(department);
            var departmentDto = _mapper.Map<DepartmentDto>(department);
            return /*CreatedAtAction(nameof(GetDepartmentByIdAsync), new { id = department.Id }*/ departmentDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartmentAsync(Guid id, DepartmentUpdateDto updateDepartmentDto)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            _mapper.Map(updateDepartmentDto, department);
            await _departmentRepository.UpdateDepartmentAsync(department);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartmentAsync(Guid id)
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
