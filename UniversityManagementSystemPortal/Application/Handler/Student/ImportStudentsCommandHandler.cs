using LINQtoCSV;
using MediatR;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Application.Command.Student;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Interfce;
using UniversityManagementSystemPortal.ModelDto.Student;

namespace UniversityManagementSystemPortal
{
    public class ImportStudentsCommandHandler : IRequestHandler<ImportStudentsCommand>
    {
        private readonly IUserInterface _userInterface;
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentProgramRepository _studentProgramRepository;
        private readonly IProgramRepository _programRepository;
        private readonly ILogger<ImportStudentsCommandHandler> _logger;
        private readonly IIdentityServices _identityServices;

        public ImportStudentsCommandHandler(
            IUserInterface userInterface,
            IStudentRepository studentRepository,
            IStudentProgramRepository studentProgramRepository,
            IProgramRepository programRepository,
            ILogger<ImportStudentsCommandHandler> logger,
            IIdentityServices identityServices)
        {
            _userInterface = userInterface;
            _studentRepository = studentRepository;
            _studentProgramRepository = studentProgramRepository;
            _programRepository = programRepository;
            _logger = logger;
            _identityServices = identityServices;
        }

        public async Task<Unit> Handle(ImportStudentsCommand request, CancellationToken cancellationToken)
        {
            foreach (var data in request.StudentsData)
            {
                // Create user model from CSV data
                var userModel = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = data.FirstName,
                    MiddleName = data.MiddleName,
                    LastName = data.LastName,
                    MobileNo = data.MobileNo,
                    DateOfBirth = data.DateOfBirth,
                    Gender = data.Gender,
                    Email = data.Email,
                    BloodGroup = data.BloodGroup.Value,
                    Username = data.UserName,
                    EmailConfirmed = data.EmailConfirm.Value,
                    Password = data.Password
                    // Add other properties as needed
                };

                // Save user model to database
                await _userInterface.RegisterAsUser(userModel);

                // Create student model from CSV data
                var studentModel = new Student
                {
                    Id = Guid.NewGuid(),
                    UserId = userModel.Id,
                    AdmissionNo = data.AdmissionNo,
                    Category = data.Category,
                };

                // Add student model to database
                var program = await _programRepository.GetProgramByName(data.ProgramName);
                if (program == null)
                {
                    _logger.LogWarning("Invalid program name: {ProgramName}", data.ProgramName);
                    throw new Exception("Invalid program name.");
                }
                await _studentRepository.Add(studentModel);
                await _studentProgramRepository.AddStudentProgramAsync(new StudentProgram
                {
                    Id = Guid.NewGuid(),
                    StudentId = studentModel.Id,
                    ProgramId = program.Id,
                    RoleNo = data.RoleNo,
                    IsActive = true,
                    CreatedBy = _identityServices.GetUserId().Value,
                    UpdatedBy = _identityServices.GetUserId().Value
                });
            }

            _logger.LogInformation("Processed {Count} student records from uploaded file.", request.StudentsData.Count);

            return Unit.Value;
        }
    }
}
