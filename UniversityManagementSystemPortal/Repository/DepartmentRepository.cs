using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly UmspContext _context;

        public DepartmentRepository(UmspContext context)
        {

            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            var departments = await _context.Departments
                .Include(d => d.Institute)
                .ToListAsync();

            if (departments == null)
            {
                return null;
            }

            return departments;
        }

        public async Task<Department> GetDepartmentByIdAsync(Guid departmentId)
        {
            if (departmentId == Guid.Empty)
            {
                return null;
            }

            var department = await _context.Departments
                .Include(d => d.Institute)
                .FirstOrDefaultAsync(d => d.Id == departmentId);

            if (department == null)
            {
                return null;
            }

            return department;
        }

        public async Task<IEnumerable<Department>> GetDepartmentsByInstituteIdAsync(Guid instituteId)
        {
            if (instituteId == Guid.Empty)
            {
                return null;
            }

            var departments = await _context.Departments
                .Include(d => d.Institute)
                .Where(d => d.InstituteId == instituteId)
                .ToListAsync();

            if (departments == null)
            {
                return null;
            }

            return departments;
        }

        public async Task<Department> CreateDepartmentAsync(Department department)
        {
            if (department == null)
            {
                return null;
            }

                department.Id = Guid.NewGuid();
            department.CreatedAt = DateTime.UtcNow;
            department.IsActive = false;
            department.IsAcademics = false;
            department.IsAdministrative = true;

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return department;
        }

        public async Task<Department> UpdateDepartmentAsync(Department department)
        {
            if (department == null)
            {
                throw new ArgumentNullException(nameof(department));
            }

            department.UpdatedAt = DateTime.UtcNow;

            _context.Departments.Update(department);
            await _context.SaveChangesAsync();

            return department;
        }

        public async Task DeleteDepartmentAsync(Guid departmentId)
        {
            if (departmentId == Guid.Empty)
            {
                throw new ArgumentException("Department ID cannot be empty", nameof(departmentId));
            }

            var department = await _context.Departments.FindAsync(departmentId);

            if (department == null)
            {
                throw new InvalidOperationException($"Department with ID {departmentId} not found");
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
        }
        public async Task<Department> GetDepartmentByNameAsync(string departmentName)
        {
            // Retrieve the department with the specified name
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Name == departmentName);
            return department;
        }
    }

}
