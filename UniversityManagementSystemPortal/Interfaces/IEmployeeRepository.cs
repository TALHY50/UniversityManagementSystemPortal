
namespace UniversityManagementSystemPortal.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetByIdAsync(Guid id);
        IQueryable<Employee> GetAllAsync();
        Task<IEnumerable<Employee>> GetByDepartmentIdAsync(Guid departmentId);
        Task<IEnumerable<Employee>> GetByPositionIdAsync(Guid positionId);
        Task<Employee> AddAsync(Employee employee);
        Task<Employee> UpdateAsync(Employee employee);
        Task DeleteAsync(Guid id);
        Task<bool> EmployeeNoExistsAsync(string employeeNo);
        Task<bool> SaveChangesAsync();
    }
}
