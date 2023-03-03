using Microsoft.EntityFrameworkCore;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly UmspContext _context;

        public DepartmentRepository(UmspContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            var departments = await _context.Departments
                .Include(d => d.Institute)
                .ToListAsync();

            if (departments == null)
            {
                throw new InvalidOperationException("Unable to retrieve departments");
            }

            return departments;
        }

        public async Task<Department> GetDepartmentByIdAsync(Guid departmentId)
        {
            if (departmentId == Guid.Empty)
            {
                throw new ArgumentException("Department ID cannot be empty", nameof(departmentId));
            }

            var department = await _context.Departments
                .Include(d => d.Institute)
                .FirstOrDefaultAsync(d => d.Id == departmentId);

            if (department == null)
            {
                throw new InvalidOperationException($"Department with ID {departmentId} not found");
            }

            return department;
        }

        public async Task<IEnumerable<Department>> GetDepartmentsByInstituteIdAsync(Guid instituteId)
        {
            if (instituteId == Guid.Empty)
            {
                throw new ArgumentException("Institute ID cannot be empty", nameof(instituteId));
            }

            var departments = await _context.Departments
                .Include(d => d.Institute)
                .Where(d => d.InstituteId == instituteId)
                .ToListAsync();

            if (departments == null)
            {
                throw new InvalidOperationException($"Unable to retrieve departments for institute with ID {instituteId}");
            }

            return departments;
        }

        public async Task<Department> CreateDepartmentAsync(Department department)
        {
            if (department == null)
            {
                throw new ArgumentNullException(nameof(department));
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
    }

}
