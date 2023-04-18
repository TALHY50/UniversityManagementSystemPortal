using Microsoft.EntityFrameworkCore;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Models.DbContext;

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
             
                .SingleOrDefaultAsync(i => i.Id == id);

            if (institute == null)
            {
                
            }

            return institute;
        }

        public IQueryable<Institute> GetAllAsync()
        {
            var institutes = _context.Institutes.AsNoTracking();

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
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(Institute institute)
        {
            if (institute == null)
            {
                throw new ArgumentNullException(nameof(institute));
            }
            
            _context.Institutes.Update(institute);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var institute = await GetByIdAsync(id);

            if (institute == null)
            {
                throw new ArgumentException($"No institute found with ID {id}");
            }

            _context.Institutes.Remove(institute);
            await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }

}
