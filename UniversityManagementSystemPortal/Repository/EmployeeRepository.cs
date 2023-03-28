using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Employee;
using UniversityManagementSystemPortal.Models.DbContext;

namespace UniversityManagementSystemPortal.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly UmspContext _dbContext;
        private readonly IUserInterface _userInterface;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IIdentityServices _identityServices;
        private readonly IInstituteAdminRepository _instituteAdminRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        public EmployeeRepository(IUserRoleRepository userRoleRepository,IPositionRepository positionRepository, IInstituteAdminRepository instituteAdminRepository, IIdentityServices identityServices, IDepartmentRepository departmentRepository, IUserInterface userInterface,UmspContext dbContext)
        {
            _dbContext = dbContext;
           _departmentRepository = departmentRepository;
            _identityServices = identityServices;
            _instituteAdminRepository = instituteAdminRepository;
            _userInterface = userInterface;
            _positionRepository = positionRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<Employee> GetByIdAsync(Guid id)
        {
            var employee = await _dbContext.Employees
                .Include(e => e.Department)
                .Include(e => e.Institute)
                .Include(e => e.Position)
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.Id == id);
            employee.ProfilePath = GetProfilePicturePath(employee.ProfilePath);
            return employee;

        }

        public  IQueryable<Employee> GetAllAsync()
        {
            var employees = _dbContext.Employees.AsQueryable()
                .Include(e => e.Department)
                .Include(e => e.Institute)
                .Include(e => e.Position)
                .Include(e => e.User).AsNoTracking();
            return employees;
        }

        public async Task<IEnumerable<Employee>> GetByDepartmentIdAsync(Guid departmentId)
        {
            var employees = await _dbContext.Employees
                .Include(e => e.Department)
                .Include(e => e.Institute)
                .Include(e => e.Position)
                .Include(e => e.User)
                .Where(e => e.DepartmentId == departmentId)
                .ToListAsync();

            return employees ?? Enumerable.Empty<Employee>();
        }

        public async Task<IEnumerable<Employee>> GetByPositionIdAsync(Guid positionId)
        {
            var employees = await _dbContext.Employees
                .Include(e => e.Department)
                .Include(e => e.Institute)
                .Include(e => e.Position)
                .Include(e => e.User)
                .Where(e => e.PositionId == positionId)
                .ToListAsync();

            return employees ?? Enumerable.Empty<Employee>();
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
            if (employee == null)
            {
                return null;
            }

            await _dbContext.Employees.AddAsync(employee);
            await SaveChangesAsync();
            return employee;
        }


        public async Task<Employee> UpdateAsync(Employee employee)
        {
            if (employee == null)
            {
                return null;
            }
            _dbContext.Employees.Update(employee);
            await SaveChangesAsync();
            return employee;
        }

        
        public async Task DeleteAsync(Guid id)
        {
            var employee = await GetByIdAsync(id);

            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                await SaveChangesAsync();
            }
        }
        public async Task<bool> EmployeeNoExistsAsync(string employeeNo)
        {
            return await _dbContext.Employees.AnyAsync(x => x.EmployeeNo == employeeNo);
        }
        private string GetProfilePicturePath(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                return Path.Combine("uploads", filePath);
            }

            return null;
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<List<string>> Upload(List<EmployeeReadModel> employeessData)
        {
            var skippedEntries = new List<string>();

            for (int i = 0; i < employeessData.Count(); i++)
            {
                var employeeData = employeessData.ElementAt(i);

                if (employeeData.Username == null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value Username Can not be null");
                    continue;
                }
                //if (studentData.InstituteName == null)
                //{
                //    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value InstituteName Can not be null");
                //    continue;
                //}
                if (employeeData.DepartmentName == null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value DepartmentName Can not be null");
                    continue;
                }
                if (employeeData.Password == null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value Password Can not be null");
                    continue;
                }
                if (employeeData.Gender == null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value GenderName Can not be null");
                    continue;
                }
                if (employeeData.DateOfBirth == null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value DateOfBirth Can not be null");
                    continue;
                }
                if (employeeData.FirstName == null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value FirstName Can not be null");
                    continue;
                }
                if (employeeData.EmployeeNo == null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value EmployeeNo Can not be null");
                    continue;
                }
                if (employeeData.PositionName == null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value PositionName Can not be null");
                    continue;
                }
                if (employeeData.Email == null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value Email Can not be null");
                    continue;
                }

                // Get the department based on the user input
                var departmentName = _dbContext.Departments.FirstOrDefault(p => p.Name == employeeData.DepartmentName);
                if (departmentName == null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: DepartmentName does'nt exist against this name");
                    continue;
                }

                var email = _dbContext.Users.FirstOrDefault(p => p.Email == employeeData.Email);

                if (email != null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Email cannot be duplicated");
                    continue;
                }

                var employeeNo = _dbContext.Employees.FirstOrDefault(p => p.EmployeeNo == employeeData.EmployeeNo);

                if (employeeNo != null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Employee already exist against this EmployeeNo");
                    continue;
                }

                var userName = _dbContext.Users.FirstOrDefault(p => p.Username == employeeData.Username);

                if (userName != null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: userName already exist.");
                    continue;
                }
                var activeInstituteId = await _instituteAdminRepository.GetInstituteIdByActiveUserId(_identityServices.GetUserId().Value);
                var userModel = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = employeeData.FirstName,
                    MiddleName = employeeData.MiddleName,
                    LastName = employeeData.LastName,
                    MobileNo = employeeData.MobileNo,
                    DateOfBirth = employeeData.DateOfBirth,
                    Gender = employeeData.Gender,
                    Email = employeeData.Email,
                    BloodGroup = employeeData.BloodGroup,
                    Username = employeeData.Username,
                    Password = employeeData.Password,
                    EmailConfirmed =employeeData.EmailConfirmed,

                    CreatedBy = _identityServices.GetUserId(),
                    UpdatedBy = _identityServices.GetUserId()
                };

                // Save user model to database
                await _userInterface.RegisterAsUser(userModel);

                // Retrieve department from database
                var department = await _departmentRepository.GetDepartmentByNameAsync(employeeData.DepartmentName);
                if (department == null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value PositionName Can not be Found");
                    continue;
                }
                // Retrieve position from database
                var position = await _positionRepository.GetPositionByName(employeeData.PositionName);
                if (position == null)
                {
                    skippedEntries.Add($"Skipped Row {i + 1}  Reason: Required value PositionName Can not be Found");
                    continue;
                }
                // Create employee model from CSV data
                var employeeModel = new Employee
                {
                    Id = Guid.NewGuid(),
                    EmployeeNo = employeeData.EmployeeNo,
                    EmployeeType = employeeData.EmployeeType,
                    Address = employeeData.EmployeAddress,
                    JoiningDate = employeeData.JoiningDate,
                    IsActive = employeeData.IsActive,
                    DepartmentId = department.Id,
                    PositionId = position.Id,
                    UserId = userModel.Id,
                    InstituteId = activeInstituteId.Value,
                    CreatedBy = _identityServices.GetUserId(),
                    UpdatedBy = _identityServices.GetUserId()
                };

                // Save employee model to database
                var addEmployee =await AddAsync(employeeModel);

                if (employeeData.EmployeeType == EmployeeType.Staff)
                {
                    var staffRole = _dbContext.Roles.FirstOrDefault(r => r.Name == "Staff");
                    var staffUserRole = new UserRole
                    {
                        Id = Guid.NewGuid(),
                        UserId = userModel.Id,
                        RoleId = staffRole.Id
                    };
                    await _userRoleRepository.AddAsync(staffUserRole);
                }
                else if (employeeData.EmployeeType == EmployeeType.Faculty)
                {
                    var facultyRole = _dbContext.Roles.FirstOrDefault(r => r.Name == "Faculty");
                    var facultyUserRole = new UserRole
                    {
                        Id = Guid.NewGuid(),
                        UserId = userModel.Id,
                        RoleId = facultyRole.Id
                    };
                    await _userRoleRepository.AddAsync(facultyUserRole);
                }

                if (addEmployee == null)
                {
                    skippedEntries.Add($"Skipped entry with Employee No: {employeeData.EmployeeNo}. Reason: Required value(s) missing. Line: {i + 2}");
                }
            }
            return skippedEntries.ToList();
        }
    }


}
