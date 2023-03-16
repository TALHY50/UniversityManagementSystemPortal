using AutoMapper;
using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Authorization.UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.CsvImport;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Student;
using UniversityManagementSystemPortal.ModelDto.UserDto;
using UniversityManagementSystemPortal.PictureManager;
using UniversityManagementSystemPortal.Repository;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System.Text;
using System.Collections;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfce;

namespace UniversityManagementSystemPortal.Controllers
{
    [JwtAuthorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        private readonly IUserInterface _userInterface;
        private readonly IInstituteRepository _instituteRepository;
        private readonly IPictureManager _pictureManager;
        private readonly ImportExportService<StudentDto> _importExportService;
        private readonly IIdentityServices _identityServices;
        private readonly ILogger<StudentController> _logger;
        private readonly IInstituteAdminRepository _repository;
        public StudentController(ILogger<StudentController> logger,
            IMapper mapper,
            IStudentRepository studentRepository,
            IPictureManager pictureManager,
            IInstituteRepository instituteRepository,
            IUserInterface userInterface,
            IInstituteAdminRepository repository,
        ImportExportService<StudentDto> importExportService,
            IIdentityServices identityServices)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
            _pictureManager = pictureManager;
            _importExportService = importExportService;
            _identityServices = identityServices;
            _userInterface = userInterface;
            _logger = logger;
            _instituteRepository = instituteRepository;
            _repository = repository;

        }
        [JwtAuthorize("Students", "Admin", "SuperAdmin", "Teacher")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var students = await _studentRepository.Get();
            var studentDtos = _mapper.Map<List<StudentDto>>(students);
            return Ok(studentDtos);
        }
        [JwtAuthorize("Admin", "SuperAdmin")]
        [HttpGet("export")]
        public async Task<IActionResult> ExportToCsv()
        {
            var students = await _studentRepository.Get();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                Encoding = System.Text.Encoding.UTF8
            };

            var studentReadModels = _mapper.Map<IEnumerable<StudentReadModel>>(students);

            using (var writer = new StringWriter())
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(studentReadModels);
                return File(System.Text.Encoding.UTF8.GetBytes(writer.ToString()), "text/csv", "students.csv");
            }
        }
        [JwtAuthorize("Admin", "SuperAdmin")]
        [HttpPost("Import")]
        public async Task<IActionResult> ImportStudents(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest(new { message = "Please select a valid file." });
            }

            if (!file.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new { message = "Invalid file format. Please select a CSV file." });
            }

            try
            {
                var instituteAdminId = _identityServices.GetUserId().GetValueOrDefault();
                var instituteAdmin = await _repository.GetByUserIdAsync(instituteAdminId);

                if (instituteAdmin == null)
                {
                    return BadRequest(new { message = "Invalid user. No associated institute found." });
                }

                using (var stream = file.OpenReadStream())
                using (var reader = new StreamReader(stream))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<StudentReadModel>();
                    var students = _mapper.Map<IEnumerable<Student>>(records);

                    foreach (var student in students)
                    {
                        student.InstituteId = instituteAdmin.InstituteId;
                        student.CreatedBy = instituteAdminId;
                        await _studentRepository.Add(student);
                    }
                }

                return Ok(new { message = "Students added successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while importing students.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while importing students. Please try again later." });
            }
        }
        [JwtAuthorize("Students", "Admin", "SuperAdmin", "Teacher")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var student = await _studentRepository.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            var studentDto = _mapper.Map<StudentDto>(student);
            return Ok(studentDto);
        }
        [JwtAuthorize("Admin", "SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AddStudentDto addStudentDto, IFormFile picture)
        {
            if (addStudentDto == null)
            {
                return BadRequest("The student data is empty.");
            }

            if (addStudentDto.InstituteId == null)
            {
                return BadRequest("The Institute ID is required.");
            }

            // Check if the student with the same admission number already exists
            var existingStudent = await _studentRepository.GetByAdmissionNo(addStudentDto.AdmissionNo);
            if (existingStudent != null)
            {
                return Conflict("A student with the same admission number already exists.");
            }

            var student = _mapper.Map<Student>(addStudentDto);

            if (picture != null)
            {
                student.ProfilePath = await _pictureManager.Upload(picture);
            }

            var addedStudent = await _studentRepository.Add(student);
            var addedStudentDto = _mapper.Map<StudentDto>(addedStudent);
            return Ok(addedStudentDto);
        }


        [HttpGet("{admissionNo}")]
        public async Task<IActionResult> GetByAdmissionNo(string admissionNo)
        {
            var student = await _studentRepository.GetByAdmissionNo(admissionNo);
            if (student == null)
            {
                return NotFound();
            }
            var studentDto = _mapper.Map<StudentDto>(student);
            return Ok(studentDto);
        }
        [JwtAuthorize("Admin", "SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] UpdateStudentDto updateStudentDto)
        {
            var existingStudent = await _studentRepository.GetById(id);
            if (existingStudent == null)
            {
                return NotFound();
            }
            var updatedStudent = _mapper.Map(updateStudentDto, existingStudent);
            var updatedStudentDto = _mapper.Map<StudentDto>(updatedStudent);
            return Ok(updatedStudentDto);
        }
        [JwtAuthorize("Admin", "SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingStudent = await _studentRepository.GetById(id);
            if (existingStudent == null)
            {
                return NotFound();
            }

            // Delete profile picture
            if (!string.IsNullOrEmpty(existingStudent.ProfilePath))
            {
                _pictureManager.Delete(existingStudent.ProfilePath);
            }

            await _studentRepository.Delete(id);
            return NoContent();
        }


    }
}

