using Microsoft.EntityFrameworkCore;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Models.DbContext;
using UniversityManagementSystemPortal.Models.ModelDto.Student;
using UniversityManagementSystemPortal.PictureManager;

namespace UniversityManagementSystemPortal.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IIdentityServices _identityService;
        private readonly UmspContext _dbContext;
        private readonly IUserInterface _userInterface;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IPictureManager _pictureManager;
        private readonly IStudentProgramRepository _studentProgramRepository;
        public StudentRepository(UmspContext dbContext, IUserRoleRepository userRoleRepository, IStudentProgramRepository studentProgramRepository, IUserInterface userInterface, IPictureManager pictureManager, IIdentityServices identityService)
        {
            _dbContext = dbContext;
            _pictureManager = pictureManager;
            _identityService = identityService;
            _userInterface = userInterface;
            _studentProgramRepository = studentProgramRepository;
            _userRoleRepository = userRoleRepository;
        }
        public IQueryable<Student> Get()
        {
            return _dbContext.Students.AsQueryable().Include(s => s.User)
                .Include(s => s.Institute)
                .Include(s => s.StudentPrograms)
                    .ThenInclude(sp => sp.Program)
                        .ThenInclude(p => p.Department).AsNoTracking();

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
            await SaveChangesAsync();
            return student;
        }
        public async Task<Student> Update(Student student)
        {

            student.Id = Guid.NewGuid();
            _dbContext.Students.Update(student);
            await SaveChangesAsync();

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
            await SaveChangesAsync();
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
        public async Task<List<string>> Upload(List<StudentReadModel> studentDataList)
        {
            var skippedEntries = new List<string>();

            // Loop through each student in the list and perform the necessary checks before adding to the database
            for (int i = 0; i < studentDataList.Count(); i++)
            {
                var studentData = studentDataList.ElementAt(i);

                if (studentData.UserName == null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value Username Can not be null");
                    continue;
                }
                if (studentData.ProgramName == null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value ProgramName Can not be null");
                    continue;
                }
                if (studentData.Password == null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value Password Can not be null");
                    continue;
                }
                if (studentData.Gender.ToString() == null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value GenderName Can not be null");
                    continue;
                }
                if (studentData.DateOfBirth == null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value DateOfBirth Can not be null");
                    continue;
                }
                if (studentData.FirstName == null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value FirstName Can not be null");
                    continue;
                }
                if (studentData.AdmissionNo == null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value AdmissionNo Can not be null");
                    continue;
                }
                if (studentData.RoleNo == null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value RoleNo Can not be null");
                    continue;
                }
                if (studentData.Email == null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value Email Can not be null");
                    continue;
                }

                var program = _dbContext.Programs.FirstOrDefault(p => p.Name == studentData.ProgramName);
                if (program == null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Program does not exist against this program name");
                    continue;
                }

                var roleNo = _dbContext.StudentPrograms.FirstOrDefault(p => p.RoleNo == studentData.RoleNo);
                if (roleNo != null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1} Reason: Role number already exists");
                    continue;
                }

                var email = _dbContext.Users.FirstOrDefault(p => p.Email == studentData.Email);

                if (email != null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Email cannot be duplicated");
                    continue;
                }

                var addmissionNo = _dbContext.Students.FirstOrDefault(p => p.AdmissionNo == studentData.AdmissionNo);

                if (addmissionNo != null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Student already exists against this admissionNo");
                    continue;
                }

                var userName = _dbContext.Users.FirstOrDefault(p => p.Username == studentData.UserName);

                if (userName != null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: userName already exists.");
                    continue;
                }

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = studentData.FirstName,
                    MiddleName = studentData.MiddleName,
                    LastName = studentData.LastName,
                    MobileNo = studentData.MobileNo,
                    DateOfBirth = studentData.DateOfBirth,
                    Gender = studentData.Gender,
                    BloodGroup = studentData.BloodGroup.Value,
                    Email = studentData.Email,
                    Username = studentData.UserName,
                    EmailConfirmed = studentData.EmailConfirm.Value,
                    IsActive = studentData.IsActive.Value,
                    Password = studentData.Password,
                };
             await  _userInterface.RegisterAsUser(user);
                var student = new Student
                {
                    Id = Guid.NewGuid(),
                    AdmissionNo = studentData.AdmissionNo,
                    Category = studentData.Category,
                    Address = studentData.Address,
                    IsActive = studentData.IsActive.Value,
                };
                var addStudent = await Add(student);
                var studentProgram = new StudentProgram
                {
                    Id = Guid.NewGuid(),
                    StudentId = student.Id,
                    ProgramId = program.Id,
                    RoleNo = studentData.RoleNo,
                    IsActive = true,
                    CreatedBy = _identityService.GetUserId(),
                    UpdatedBy = _identityService.GetUserId()
                };
              await  _studentProgramRepository.AddStudentProgramAsync(studentProgram);
                
                var role = _dbContext.Roles.FirstOrDefault(r => r.Name == "Student");

                var userRole = new UserRole
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    RoleId = role.Id
                };
               await _userRoleRepository.AddAsync(userRole);

                if (addStudent == null)
                {
                    skippedEntries.Add($"Skipped entry with Roll No: {studentData.RoleNo}. Reason: Required value(s) missing. Line: {i + 1}");
                }
            }

            return skippedEntries.ToList();
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
