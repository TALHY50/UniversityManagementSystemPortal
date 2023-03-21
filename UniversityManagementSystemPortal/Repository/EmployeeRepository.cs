using Microsoft.EntityFrameworkCore;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly UmspContext _dbContext;

        public EmployeeRepository(UmspContext dbContext)
        {
            _dbContext = dbContext;

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

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var employees = await _dbContext.Employees
                .Include(e => e.Department)
                .Include(e => e.Institute)
                .Include(e => e.Position)
                .Include(e => e.User)
                .ToListAsync();

            return employees ?? Enumerable.Empty<Employee>();
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
            await _dbContext.SaveChangesAsync();
            return employee;
        }


        public async Task<Employee> UpdateAsync(Employee employee)
        {
            if (employee == null)
            {
                return null;
            }
            _dbContext.Employees.Update(employee);
            await _dbContext.SaveChangesAsync();
            return employee;
        }

        
        public async Task DeleteAsync(Guid id)
        {
            var employee = await GetByIdAsync(id);

            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                await _dbContext.SaveChangesAsync();
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
    }


}
