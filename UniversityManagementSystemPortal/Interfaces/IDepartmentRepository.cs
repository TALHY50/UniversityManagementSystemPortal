using UniversityManagementsystem.Models;

namespace UniversityManagementSystemPortal.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task<Department> GetDepartmentByIdAsync(Guid departmentId);
        Task<IEnumerable<Department>> GetDepartmentsByInstituteIdAsync(Guid instituteId);
        Task<Department> CreateDepartmentAsync(Department department);
        Task<Department> UpdateDepartmentAsync(Department department);
        Task DeleteDepartmentAsync(Guid departmentId);
        Task<Department> GetDepartmentByNameAsync(string name);
    }
}
