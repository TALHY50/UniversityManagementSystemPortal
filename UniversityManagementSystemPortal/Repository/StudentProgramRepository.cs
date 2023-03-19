using Microsoft.EntityFrameworkCore;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Repository
{
    public class StudentProgramRepository : IStudentProgramRepository
    {
        private readonly UmspContext _dbContext;

        public StudentProgramRepository(UmspContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddStudentProgramAsync(StudentProgram studentProgram)
        {
            studentProgram.Id = Guid.NewGuid();
            await _dbContext.StudentPrograms.AddAsync(studentProgram);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<StudentProgram>> GetAllStudentProgramsAsync()
        {
            return await _dbContext.StudentPrograms
                .Include(sp => sp.Program)
                .Include(sp => sp.Student)
                .ToListAsync();
        }

        public async Task<StudentProgram> GetStudentProgramByIdAsync(Guid id)
        {
            return await _dbContext.StudentPrograms
                .Include(sp => sp.Program)
                .Include(sp => sp.Student)
                .SingleOrDefaultAsync(sp => sp.Id == id);
        }

        public async Task UpdateStudentProgramAsync(StudentProgram studentProgram)
        {
            _dbContext.StudentPrograms.Update(studentProgram);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteStudentProgramAsync(Guid id)
        {
            var studentProgram = await GetStudentProgramByIdAsync(id);
            if (studentProgram != null)
            {
                _dbContext.StudentPrograms.Remove(studentProgram);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

}
