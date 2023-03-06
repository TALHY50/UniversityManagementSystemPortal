using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityManagementsystem.Models;
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

        public StudentController(IMapper mapper, IStudentRepository studentRepository, IPictureManager pictureManager)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
            _pictureManager = pictureManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var students = await _studentRepository.Get();
            var studentDtos = _mapper.Map<List<StudentDto>>(students);
            return Ok(studentDtos);
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
    }

}
