using Microsoft.EntityFrameworkCore;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Repository
{
    public class PositionRepository : IPositionRepository
    {
        private readonly UmspContext _dbContext;

        public PositionRepository(UmspContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Position> GetByIdAsync(Guid id)
        {
            var position = await _dbContext.Positions
                .Include(p => p.Category)
                .Include(p => p.Employees)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (position == null)
            {
                // handle null reference
                throw new Exception($"Position with id {id} not found.");
            }

            return position;
        }

        public async Task<IEnumerable<Position>> GetAllAsync()
        {
            var positions = await _dbContext.Positions
                .Include(p => p.Category)
                .Include(p => p.Employees)
                .ToListAsync();

            if (positions == null)
            {
                // handle null reference
                throw new Exception("No positions found.");
            }

            return positions;
        }

        public async Task<IEnumerable<Position>> GetByCategoryIdAsync(Guid categoryId)
        {
            var positions = await _dbContext.Positions
                .Include(p => p.Category)
                .Include(p => p.Employees)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();

            if (positions == null)
            {
                // handle null reference
                throw new Exception($"No positions found with category id {categoryId}.");
            }

            return positions;
        }

        public async Task CreateAsync(Position position)
        {
            if (position == null)
            {
                // handle null reference
                throw new Exception("Position is null.");
            }

            await _dbContext.Positions.AddAsync(position);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Position position)
        {
            if (position == null)
            {
                // handle null reference
                throw new Exception("Position is null.");
            }

            _dbContext.Positions.Update(position);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var position = await GetByIdAsync(id);

            if (position == null)
            {
                // handle null reference
                throw new Exception($"Position with id {id} not found.");
            }

            _dbContext.Positions.Remove(position);
            await _dbContext.SaveChangesAsync();
        }
    }


}
