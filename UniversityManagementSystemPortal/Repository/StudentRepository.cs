using DocumentFormat.OpenXml.InkML;
using LINQtoCSV;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using NuGet.Versioning;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.FilterandSorting;
using UniversityManagementSystemPortal.Helpers.Paging;
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

        public async Task<PaginatedList<Student>> Get(PaginatedViewModel paginatedViewModel)
        {
            var students = await _dbContext.Students
                .Include(s => s.User)
                .Include(s => s.Institute)
                .Include(s => s.StudentPrograms)
                    .ThenInclude(sp => sp.Program)
                        .ThenInclude(p => p.Department)
                .ToListAsync();

            var filteredStudents = Filtering.Filter<Student, User>(paginatedViewModel.columnName, paginatedViewModel.search, students.AsQueryable(), s => s.Institute);
            var sortedStudents = Sorting<Student>.Sort(paginatedViewModel.SortBy, paginatedViewModel.columnName, filteredStudents);
            var paginatedStudents = PaginationHelper.Create(sortedStudents.AsQueryable(), paginatedViewModel);

            return paginatedStudents;
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

        public async Task<Student> Update(Student student)
        {
           
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
        //public List<string> Upload(List<StudentReadModel> studentDataList)
        //{
        //    var skippedEntries = new List<string>();

        //    // Loop through each student in the list and perform the necessary checks before adding to the database
        //    for (int i = 0; i < studentDataList.Count(); i++)
        //    {
        //        var studentData = studentDataList.ElementAt(i);

        //        if (studentData.UserName == null)
        //        {
        //            skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value Username Can not be null");
        //            continue;
        //        }
        //        if (studentData.ProgramName == null)
        //        {
        //            skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value ProgramName Can not be null");
        //            continue;
        //        }
        //        if (studentData.Password == null)
        //        {
        //            skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value Password Can not be null");
        //            continue;
        //        }
        //        if (studentData.Gender.ToString() == null)
        //        {
        //            skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value GenderName Can not be null");
        //            continue;
        //        }
        //        if (studentData.DateOfBirth == null)
        //        {
        //            skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value DateOfBirth Can not be null");
        //            continue;
        //        }
        //        if (studentData.FirstName == null)
        //        {
        //            skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value FirstName Can not be null");
        //            continue;
        //        }
        //        if (studentData.AdmissionNo == null)
        //        {
        //            skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value AdmissionNo Can not be null");
        //            continue;
        //        }
        //        if (studentData.RoleNo == null)
        //        {
        //            skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value RoleNo Can not be null");
        //            continue;
        //        }
        //        if (studentData.Email == null)
        //        {
        //            skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value Email Can not be null");
        //            continue;
        //        }

        //        var program = _dbContext.Programs.FirstOrDefault(p => p.Name == studentData.ProgramName);
        //        if (program == null)
        //        {
        //            skippedEntries.Add($"Skipped Row {i + 1}  Reason: Program does not exist against this program name");
        //            continue;
        //        }

        //        var roleNo = _dbContext.StudentPrograms.FirstOrDefault(p => p.RoleNo == studentData.RoleNo);
        //        if (roleNo != null)
        //        {
        //            skippedEntries.Add($"Skipped Row {i + 1} Reason: Role number already exists");
        //            continue;
        //        }

        //        var email = _dbContext.Users.FirstOrDefault(p => p.Email == studentData.Email);

        //        if (email != null)
        //        {
        //            skippedEntries.Add($"Skipped Row {i + 1}  Reason: Email cannot be duplicated");
        //            continue;
        //        }

        //        var addmissionNo = _dbContext.Students.FirstOrDefault(p => p.AdmissionNo == studentData.AdmissionNo);

        //        if (addmissionNo != null)
        //        {
        //            skippedEntries.Add($"Skipped Row {i + 1}  Reason: Student already exists against this admissionNo");
        //            continue;
        //        }

        //        var userName = _dbContext.Users.FirstOrDefault(p => p.Username == studentData.UserName);

        //        if (userName != null)
        //        {
        //            skippedEntries.Add($"Skipped Row {i + 1}  Reason: userName already exists.");
        //            continue;
        //        }

        //        var user = new User
        //        {
        //            FirstName = studentData.FirstName,
        //            MiddleName = studentData.MiddleName,
        //            LastName = studentData.LastName,
        //            MobileNo = studentData.MobileNo,
        //            DateOfBirth = studentData.DateOfBirth,
        //            Gender =studentData.Gender,
        //            BloodGroup = studentData.BloodGroup.Value,
        //            Email = studentData.Email,
        //            Username = studentData.UserName,
        //            EmailConfirmed = studentData.EmailConfirm.Value,
        //            IsActive = studentData.IsActive.Value,
        //            Password = studentData.Password,
        //        };

        //        var student = new Student
        //        {
        //            AdmissionNo = studentData.AdmissionNo,
        //            Category = studentData.Category,
        //            Address = studentData.Address,
        //            IsActive = studentData.IsActive.Value,
        //        };

        //        var addStudent = Add(student, user, studentData);

        //        if (addStudent == null)
        //        {
        //            skippedEntries.Add($"Skipped entry with Roll No: {studentData.RoleNo}. Reason: Required value(s) missing. Line: {i + 1}");
        //        }
        //    }

        //    return skippedEntries;
        //}

        public Student AddBulk(Student student, User user, StudentReadModel dto)
        {
          
            user.CreatedBy = _identityService.GetUserId().Value;
            user.UpdatedBy = _identityService.GetUserId().Value;

            _dbContext.Users.Add(user);

            var activeUserId = _identityService.GetUserId().GetValueOrDefault();
            var institute = _dbContext.InstituteAdmins
                                  .Where(a => a.UserId == activeUserId)
                                  .Select(a => a.Institute)
                                  .FirstOrDefault();

            student.InstituteId = institute.Id;
            student.ProfilePath = null;
            student.CreatedBy = _identityService.GetUserId().Value;
            student.UpdatedBy = _identityService.GetUserId().Value;

            _dbContext.Students.Add(student);

            var program = _dbContext.Programs.FirstOrDefault(p => p.Name == dto.ProgramName);
            if (program == null)
                return null;

            var studentProgram = new StudentProgram
            {
                Id = Guid.NewGuid(),
                StudentId = student.Id,
                ProgramId = program.Id,
                RoleNo = dto.RoleNo,
                IsActive = true,
                CreatedBy = _identityService.GetUserId().Value,
                UpdatedBy = _identityService.GetUserId().Value
            };

            _dbContext.StudentPrograms.Add(studentProgram);

            _dbContext.SaveChanges();
            return student;
        }
    }
}
