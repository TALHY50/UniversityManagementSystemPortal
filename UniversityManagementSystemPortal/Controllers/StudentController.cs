using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.CsvImport;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Student;
using UniversityManagementSystemPortal.PictureManager;

namespace UniversityManagementSystemPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        private readonly IPictureManager _pictureManager;
        private readonly ImportExportService<StudentDto> _importExportService;
        public StudentController(IMapper mapper, IStudentRepository studentRepository, IPictureManager pictureManager, ImportExportService<StudentDto> importExportService)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
            _pictureManager = pictureManager;

            _importExportService = importExportService;
        }

        [HttpGet("ok")]
        public async Task<IActionResult> Get()
        {
            var students = await _studentRepository.Get();
            var studentDtos = _mapper.Map<List<StudentDto>>(students);
            return Ok(studentDtos);
        }
        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            var students = await _studentRepository.Get();
            var studentDtos = _mapper.Map<List<StudentDto>>(students);

            var csvBytes = await _importExportService.ExportToCsvAsync(studentDtos);

            return File(csvBytes, "text/csv", "students.csv");
        }


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
        //[HttpPost]
        //public IActionResult ImportStudents(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //    {
        //        return BadRequest("Please select a file to upload.");
        //    }

        //    var filePath = Path.GetTempFileName();

        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        file.CopyTo(stream);
        //    }

        //    var students = ExcelHelper.ImportCsv<StudentDto>(filePath);

        //    foreach (var student in students)
        //    {
        //        _studentRepository.Add(student);
        //    };

        //    return Ok($"Imported {students.Count} students.");
        //}
    }
}

