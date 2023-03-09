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
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Employee> GetByIdAsync(Guid id)
        {
            var employee = await _dbContext.Employees
                .Include(e => e.Department)
                .Include(e => e.Institute)
                .Include(e => e.Position)
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.Id == id);

            return employee ?? throw new InvalidOperationException($"Employee with ID {id} not found.");
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

        public async Task AddAsync(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            _dbContext.Employees.Update(employee);
            await _dbContext.SaveChangesAsync();
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
    }


}
