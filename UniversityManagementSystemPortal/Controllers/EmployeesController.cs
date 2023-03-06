using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Employee;
using UniversityManagementSystemPortal.PictureManager;

namespace UniversityManagementSystemPortal.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return Ok(employeeDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return Ok(employeeDto);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> AddAsync(CreateEmployeeDto createEmployeeDto)
        {
            var employee = _mapper.Map<Employee>(createEmployeeDto);

            await _employeeRepository.AddAsync(employee);

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = employeeDto.Id }, employeeDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _mapper.Map(updateEmployeeDto, employee);

            await _employeeRepository.UpdateAsync(employee);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _employeeRepository.DeleteAsync(id);

            return NoContent();
        }
    }




}
