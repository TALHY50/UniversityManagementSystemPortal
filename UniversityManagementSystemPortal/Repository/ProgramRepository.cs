using Microsoft.EntityFrameworkCore;
using PorgramNamespace = UniversityManagementSystemPortal.Program;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Models.DbContext;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Repository
{
    public class ProgramRepository : IProgramRepository
    {
        private readonly UmspContext _context;

        public ProgramRepository(UmspContext context)
        {
            _context = context;
        }

        public async Task<PorgramNamespace> GetByIdAsync(Guid id)
        {
            var program = await _context.Programs
                .Include(p => p.Department)
                .Include(p => p.StudentPrograms)
                    .ThenInclude(sp => sp.Student)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (program == null)
            {
                throw new AppException($"Program with id {id} not found.");
            }

            return program;
        }

        public async Task<IEnumerable<PorgramNamespace>> GetAllAsync()
        {
           var program = await _context.Programs
                .Include(p => p.Department)
                .Include(p => p.StudentPrograms)
                    .ThenInclude(sp => sp.Student)
                .ToListAsync();
            if(program == null)
            {
                throw new AppException($"Program not found.");
            }
            return program;
        }

        public async Task AddAsync(PorgramNamespace program)
        {
            if (program == null)
            {
                throw new AppException(nameof(program));
            }
            program.Id = Guid.NewGuid();
            program.IsActive = false;
            _context.Programs.Add(program);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(PorgramNamespace program)
        {
            if (program == null)
            {
                throw new AppException(nameof(program));
            }

            _context.Entry(program).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var programToDelete = await _context.Programs.FindAsync(id);

            if (programToDelete == null)
            {
                throw new AppException($"Program with id {id} not found.");
            }

            _context.Programs.Remove(programToDelete);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<StudentProgram>> GetStudentProgramsAsync(Guid programId)
        {
            var program = await GetByIdAsync(programId);

            return program.StudentPrograms;
        }
        public async Task<PorgramNamespace> GetProgramByName(string programName)
        {
            return  _context.Programs.FirstOrDefault(p => p.Name == programName);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }

}
