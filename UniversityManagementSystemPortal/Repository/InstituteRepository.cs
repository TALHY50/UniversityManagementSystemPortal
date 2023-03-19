using Microsoft.EntityFrameworkCore;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Repository
{
    public class InstituteRepository : IInstituteRepository
    {
        private readonly UmspContext _context;

        public InstituteRepository(UmspContext context)
        {
            _context = context;
        }

        public async Task<Institute> GetByIdAsync(Guid id)
        {
            var institute = await _context.Institutes
                .Include(i => i.Categories)
                .Include(i => i.Departments)
                .Include(i => i.Employees).ThenInclude(e => e.Position)
                .Include(i => i.InstituteAdmins)
                .Include(i => i.Students)
                .SingleOrDefaultAsync(i => i.Id == id);

            if (institute == null)
            {
                throw new ArgumentException($"No institute found with ID {id}");
            }

            return institute;
        }

        public async Task<IEnumerable<Institute>> GetAllAsync()
        {
            var institutes = await _context.Institutes
                .Include(i => i.Categories)
                .Include(i => i.Departments)
                .Include(i => i.Employees).ThenInclude(e => e.Position)
                .Include(i => i.InstituteAdmins)
                .Include(i => i.Students)
                .ToListAsync();

            return institutes;
        }

        public async Task AddAsync(Institute institute)
        {
            if (institute == null)
            {
                throw new ArgumentNullException(nameof(institute));
            }
            institute.Id = Guid.NewGuid();
            await _context.Institutes.AddAsync(institute);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Institute institute)
        {
            if (institute == null)
            {
                throw new ArgumentNullException(nameof(institute));
            }
            
            _context.Institutes.Update(institute);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var institute = await GetByIdAsync(id);

            if (institute == null)
            {
                throw new ArgumentException($"No institute found with ID {id}");
            }

            _context.Institutes.Remove(institute);
            await _context.SaveChangesAsync();
        }


    }

}
