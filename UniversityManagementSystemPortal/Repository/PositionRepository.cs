using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Models.DbContext;

namespace UniversityManagementSystemPortal.Repository
{
    public class PositionRepository : IPositionRepository
    {
        private readonly UmspContext _dbContext;

        public PositionRepository(UmspContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Position>> GetByIdAsync(Guid id, Guid? instituteId = null)
        {
            IQueryable<Position> query = _dbContext.Positions
                .Include(p => p.Category)
                .Include(p => p.Employees)
                .Where(p => p.Id == id);

            if (instituteId.HasValue)
            {
                query = query.Where(p => p.Category.InstituteId == instituteId);
            }

            var positions = await query.ToListAsync();

            if (positions == null || positions.Count == 0)
            {
                return null;
            }

            return positions;
        }

        public async Task<IQueryable<Position>> GetAllAsync(Guid? instituteId = null)
        {
            IQueryable<Position> query = _dbContext.Positions.AsQueryable();
            if (instituteId.HasValue)
            {
                query = query.Where(p => p.Category.InstituteId == instituteId);
            }
            return  query.AsNoTracking();
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
              
                throw new AppException($"No positions found with category id {categoryId}.");
            }

            return positions;
        }

        public async Task CreateAsync(Position position)
        {
            if (position == null)
            {
                
                throw new AppException("Position is null.");
            }
            position.Id = Guid.NewGuid();
            position.IsActive = true;
            await _dbContext.Positions.AddAsync(position);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(Position position, Guid? instituteId = null)
        {
            if (position == null)
            {
                throw new AppException("Position is null.");
            }

            // Check if the position belongs to the active institute
            if (instituteId.HasValue && position.Category.InstituteId != instituteId)
            {
                throw new AppException("You are not authorized to update this position.");
            }

            _dbContext.Positions.Update(position);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var positions = await GetByIdAsync(id);

            if (positions == null)
            {
                throw new AppException($"Position with id {id} not found.");
            }

            _dbContext.Positions.RemoveRange(positions.ToList());
            await SaveChangesAsync();
        }

        public async Task<Position> GetPositionByName(string positionName)
        {
            var position =  _dbContext.Positions.FirstOrDefault(p => p.Name == positionName);
            return position;
        }
        public async Task<IEnumerable<Position>> GetByInstituteIdAsync(Guid? instituteId = null)
        {
            var query = _dbContext.Positions.AsQueryable();

            if (instituteId.HasValue)
            {
                // Filter by institute ID
                query = query.Where(p => p.Category.InstituteId == instituteId.Value);
            }

            // Retrieve all positions
            var positions = await query.ToListAsync();

            return positions;
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task DeleteAsync(Position position)
        {
            _dbContext.Positions.Remove(position);
            await SaveChangesAsync();
        }

    }


}
