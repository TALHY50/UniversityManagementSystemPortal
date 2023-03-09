﻿using Microsoft.EntityFrameworkCore;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly UmspContext _context;

        public CategoryRepository(UmspContext context)
        {
            _context = context;
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            var category = await _context.Categories
                .Include(c => c.Institute)
                .Include(c => c.Positions)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                throw new InvalidOperationException($"Category with ID {id} not found.");
            }

            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var categories = await _context.Categories
                .Include(c => c.Institute)
                .Include(c => c.Positions)
                .ToListAsync();

            return categories ?? Enumerable.Empty<Category>();
        }

        public async Task<IEnumerable<Category>> GetByInstituteIdAsync(Guid instituteId)
        {
            var categories = await _context.Categories
                .Include(c => c.Institute)
                .Include(c => c.Positions)
                .Where(c => c.InstituteId == instituteId)
                .ToListAsync();

            return categories ?? Enumerable.Empty<Category>();
        }

        public async Task<Category> AddAsync(Category category)
        {
            if (category == null)
            {
                throw new AppException(nameof(category));
            }

            category.Id = Guid.NewGuid();
            category.IsActive = true;
            category.IsStaff = false;
            category.IsFaculty = false;
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }


        public async Task<Category> UpdateAsync(Category category)
        {
            if (category == null)
            {
                throw new AppException(nameof(category));
            }

            var existingCategory = await GetByIdAsync(category.Id);
            if (existingCategory == null)
            {
                throw new InvalidOperationException($"Category with ID {category.Id} not found.");
            }

            existingCategory.Name = category.Name;
            existingCategory.IsActive = category.IsActive;
            existingCategory.IsStaff = category.IsStaff;
            existingCategory.IsFaculty = category.IsFaculty;

            _context.Categories.Update(existingCategory);
            await _context.SaveChangesAsync();

            return existingCategory;
        }


        public async Task DeleteAsync(Category category)
        {
            if (category == null)
            {
                throw new AppException(nameof(category));
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }


}
