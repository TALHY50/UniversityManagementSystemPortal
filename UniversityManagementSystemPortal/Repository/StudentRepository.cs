using LINQtoCSV;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Student;
using UniversityManagementSystemPortal.PictureManager;

namespace UniversityManagementSystemPortal.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IIdentityServices _identityService;
        private readonly UmspContext _dbContext;
        private readonly IPictureManager _pictureManager;

        public StudentRepository(UmspContext dbContext, IPictureManager pictureManager, IIdentityServices identityService)
        {
            _dbContext = dbContext;
            _pictureManager = pictureManager;
            _identityService = identityService;
        }

        public async Task<List<Student>> Get()
        {
            var students = await _dbContext.Students
                .Include(s => s.User)
                .Include(s => s.Institute)
                .Include(s => s.StudentPrograms)
                    .ThenInclude(sp => sp.Program)
                        .ThenInclude(p => p.Department)
                .ToListAsync();

            return students;
        }

        public async Task<Student> GetById(Guid id)
        {
            var student = await _dbContext.Students
                .Include(s => s.User)
                .Include(s => s.Institute)
                .Include(s => s.StudentPrograms)
                    .ThenInclude(sp => sp.Program)
                        .ThenInclude(p => p.Department)
                .FirstOrDefaultAsync(s => s.Id == id);

            student.ProfilePath = GetProfilePicturePath(student.ProfilePath);

            return student;
        }

        public async Task<Student> Add(Student student)
        {

            student.Id = Guid.NewGuid();
            _dbContext.Students.Add(student);
            await _dbContext.SaveChangesAsync();
            return student;
        }

        public async Task<StudentReadModel> AddToImport(StudentReadModel student)
        {
            var user = new User
            {
                FirstName = student.FirstName,
                MiddleName = student.MiddleName,
                LastName = student.LastName,
                MobileNo = student.MobileNo,
                DateOfBirth = student.DateOfBirth,
                Gender = student.Gender,
                BloodGroup = student.BloodGroup.Value,
                Email = student.Email,
                Username = student.UserName,
                EmailConfirmed = true,
                IsActive = true,
                Password = student.Password,
                CreatedBy = _identityService.GetUserId().Value,
                UpdatedBy = _identityService.GetUserId().Value
            };
            _dbContext.Users.Add(user);

            var studentRole = _dbContext.Roles.FirstOrDefault(r => r.Name == "Students");
            var userRole = new UserRole
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                RoleId = studentRole.Id
            };
            _dbContext.UserRoles.Add(userRole);
            // Get the program based on the user input
            var program = _dbContext.Programs.FirstOrDefault(p => p.Name == student.ProgramName);
            if (program == null)
            {

            }
            // Get the institute entity by id
            var activeUserId = _identityService.GetUserId().Value;
            var institute = _dbContext.InstituteAdmins
                                .Where(a => a.UserId == activeUserId)
                                .Select(a => a.Institute)
                                .FirstOrDefault();

            if (institute == null)
            {

            }
            var students = new Student
            {
                AdmissionNo = student.AdmissionNo,
                Category = student.Category,
                Address = student.Address,
                InstituteId = institute.Id,
                IsActive = true,
                UserId = user.Id,
                UpdatedBy = _identityService.GetUserId().Value,
                CreatedBy = _identityService.GetUserId().Value
            };
            _dbContext.Students.AddRange(students);
             _dbContext.SaveChanges(); // Add this line to save the changes to the database
            return student;
        }



        public async Task<Student> Update(Student student, IFormFile picture)
        {
            if (picture != null)
            {
                student.ProfilePath = await _pictureManager.Update(student.Id, picture, student.ProfilePath);
            }
            student.Id = Guid.NewGuid();

            _dbContext.Students.Update(student);
            await _dbContext.SaveChangesAsync();

            student.ProfilePath = GetProfilePicturePath(student.ProfilePath);

            return student;
        }

        public async Task Delete(Guid id)
        {
            var student = await GetById(id);

            if (!string.IsNullOrEmpty(student.ProfilePath))
            {
                _pictureManager.Delete(student.ProfilePath);
            }

            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
        }

        private string GetProfilePicturePath(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                return Path.Combine("uploads", filePath);
            }

            return null;
        }
        public async Task<Student> GetByAdmissionNo(string admissionNo)
        {
            return await _dbContext.Students.FirstOrDefaultAsync(s => s.AdmissionNo == admissionNo);
        }
        public (string message, List<string> skippedEntries) Upload(IFormFile file)
        {
            var skippedEntries = new List<string>();

            if (file == null || file.Length == 0)
                return ("File is empty.", skippedEntries);

            if (!Path.GetExtension(file.FileName).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                return ("Invalid file format. Only CSV files are allowed.", skippedEntries);

            var csvFileDescription = new CsvFileDescription
            {
                FirstLineHasColumnNames = true,
                IgnoreUnknownColumns = true,
                SeparatorChar = ',',
                UseFieldIndexForReadingData = false
            };

            using (var streamReader = new StreamReader(file.OpenReadStream()))
            {
                var csvContext = new LINQtoCSV.CsvContext();
                var studentsData = csvContext.Read<StudentReadModel>(streamReader, csvFileDescription);

                for (int i = 0; i < studentsData.Count(); i++)
                {
                    var studentData = studentsData.ElementAt(i);

                    try
                    {
                        if (string.IsNullOrEmpty(studentData.AdmissionNo))
                        {
                            skippedEntries.Add($"Skipped Row {i + 1} Reason: Required value AdmissionNo cannot be null or empty");
                            continue;
                        }
                        if (string.IsNullOrEmpty(studentData.RoleNo))
                        {
                            skippedEntries.Add($"Skipped Row {i + 1} Reason: Required value RoleNo cannot be null or empty");
                            continue;
                        }
                        if (string.IsNullOrEmpty(studentData.FirstName))
                        {
                            skippedEntries.Add($"Skipped Row {i + 1} Reason: Required value FirstName cannot be null or empty");
                            continue;
                        }
                        if (studentData.Password == null)
                        {
                            skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value Password Can not be null");
                            continue;
                        }
                        if (studentData.UserName == null)
                        {
                            skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value Username Can not be null");
                            continue;
                        }
                        if (studentData.DateOfBirth==null)
                        {
                            skippedEntries.Add($"Skipped Row {i + 1} Reason: Required value DateOfBirth cannot be null or empty");
                            continue;
                        }
                        if (string.IsNullOrEmpty(studentData.Gender.ToString()))
                        {
                            skippedEntries.Add($"Skipped Row {i + 1} Reason: Required value Gender cannot be null or empty");
                            continue;
                        }
                        if (string.IsNullOrEmpty(studentData.Email))
                        {
                            skippedEntries.Add($"Skipped Row {i + 1} Reason: Required value Email cannot be null or empty");
                            continue;
                        }
                        if (string.IsNullOrEmpty(studentData.Category.ToString()))
                        {
                            skippedEntries.Add($"Skipped Row {i + 1} Reason: Required value Category cannot be null or empty");
                            continue;
                        }
                        // Get the program based on the user input
                        var program = _dbContext.StudentPrograms.FirstOrDefault(p => p.Program.Name == studentData.ProgramName);
                        if (program == null)
                        {
                            skippedEntries.Add($"Skipped Row {i + 1} Reason: Program does not exist against this program name");
                            continue;
                        }

                        var email = _dbContext.Users.FirstOrDefault(p => p.Email == studentData.Email);
                        if (email != null)
                        {
                            skippedEntries.Add($"Skipped Row {i + 1} Reason: Email already exists");
                            continue;
                        }
                        var password = _dbContext.Users.FirstOrDefault(p => p.Password == studentData.Password);
                        if (password != null)
                        {
                            skippedEntries.Add($"Skipped Row {i + 1} Reason: Password already exists");
                            continue;
                        }
                        var userName = _dbContext.Users.FirstOrDefault(p => p.Username == studentData.UserName);
                        if (userName != null)
                        {
                            skippedEntries.Add($"Skipped Row {i + 1} Reason: Password already exists");
                            continue;
                        }
                        var admissionNo = _dbContext.Students.FirstOrDefault(p => p.AdmissionNo == studentData.AdmissionNo);
                        if (admissionNo != null)
                        {
                            skippedEntries.Add($"Skipped Row {i + 1} Reason: Admission number already exists");
                            continue;
                        }
                        var roleNo = _dbContext.StudentPrograms.FirstOrDefault(p => p.RoleNo == studentData.RoleNo);
                        if (roleNo != null)
                        {
                            skippedEntries.Add($"Skipped Row {i + 1} Reason: Role number already exists");
                            continue;
                        }

                        // Create the student
                        var student = AddToImport(studentData);
                    }
                    catch (Exception ex)
                    {
                        skippedEntries.Add($"Skipped entry with Roll No: {studentData.RoleNo}. Reason: {ex.Message}. Line: {i + 2}");
                    }
                }
                if (skippedEntries.Any())
                {
                    var message = $"File imported successfully with {skippedEntries.Count} skipped entries.\n\n";
                    message += string.Join("\n\n", skippedEntries);
                    return (message, skippedEntries);
                }
            }

            return ("File uploaded successfully.", skippedEntries);














        }
    }
}
