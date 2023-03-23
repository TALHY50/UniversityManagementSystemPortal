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
    public class ImportStudentsCommandHandler : IRequestHandler<ImportStudentsCommand, List<string>>
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

        public async Task<List<string>> Handle(ImportStudentsCommand request, CancellationToken cancellationToken)
        {
            var i = 0;
            var _skippedEntries = new List<string>();
            foreach (var data in request.StudentsData)
            {
                // Check if any required value is null
                if (string.IsNullOrEmpty(data.UserName) ||
                    string.IsNullOrEmpty(data.ProgramName) ||
                    string.IsNullOrEmpty(data.Password) ||
                    string.IsNullOrEmpty(data.Gender.ToString()) ||
                    data.DateOfBirth == null ||
                    string.IsNullOrEmpty(data.FirstName) ||
                    string.IsNullOrEmpty(data.AdmissionNo) ||
                    string.IsNullOrEmpty(data.RoleNo) ||
                    string.IsNullOrEmpty(data.Email))
                {
                    // Add skipped entry to the list
                    _skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required field(s) missing.");
                    continue;
                }
                i++;

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
                    // Add skipped entry to the list
                    _skippedEntries.Add($"Skipped Row {i + 1}  Reason: Invalid program name.");
                    continue;
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

            // Log the processed records and skipped entries
            _logger.LogInformation("Processed {Count} student records from uploaded file. Skipped {SkippedCount} entries.",
                request.StudentsData.Count, _skippedEntries.Count);

            // Return skipped entries
            return _skippedEntries;
        }

    }
}
