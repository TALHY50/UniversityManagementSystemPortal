using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.CsvImport;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Employee;

namespace UniversityManagementSystemPortal.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly ImportExportService<EmployeeDto> _importExportService;
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IIdentityServices _identityServices;
        public EmployeeController(IIdentityServices identityServices, IMapper mapper, IEmployeeRepository employeeRepository, ImportExportService<EmployeeDto> importExportService)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _importExportService = importExportService;
            _identityServices = identityServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
        {
            var employees = await _employeeRepository.GetAllAsync();
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return Ok(employeeDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetById(Guid id)
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
        public async Task<ActionResult<EmployeeDto>> Add([FromForm] CreateEmployeeDto createEmployeeDto, IFormFile picture)
        {
            var userId = _identityServices.GetUserId();
            if (userId == null)
            {
                return Unauthorized(new { message = "You must be logged in to perform this action." });
            }

            var employee = _mapper.Map<Employee>(createEmployeeDto);
            employee.CreatedBy = userId.Value;

            await _employeeRepository.AddAsync(employee);

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return CreatedAtAction(nameof(GetById), new { id = employeeDto.Id }, employeeDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var userId = _identityServices.GetUserId();
            if (userId == null)
            {
                return Unauthorized(new { message = "You must be logged in to perform this action." });
            }

            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _mapper.Map(updateEmployeeDto, employee);
            employee.UpdatedBy = userId.Value;

            await _employeeRepository.UpdateAsync(employee);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _employeeRepository.DeleteAsync(id);

            return NoContent();
        }
        [HttpPost("Export")]
        public async Task<IActionResult> Export()
        {
            var students = await _employeeRepository.GetAllAsync();
            var studentDtos = _mapper.Map<List<EmployeeReadDto>>(students);
            var studentDtosEnumerable = studentDtos.Select(s => _mapper.Map<EmployeeDto>(s));
            var csvBytes = await _importExportService.ExportToCsvAsync(studentDtosEnumerable);


            return File(csvBytes, "text/csv", "students.csv");
        }
    }




}
