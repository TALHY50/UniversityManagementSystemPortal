using UniversityManagementsystem.Models;

namespace UniversityManagementSystemPortal.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetByIdAsync(Guid id);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<IEnumerable<Employee>> GetByDepartmentIdAsync(Guid departmentId);
        Task<IEnumerable<Employee>> GetByPositionIdAsync(Guid positionId);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Guid id);
    }
}
