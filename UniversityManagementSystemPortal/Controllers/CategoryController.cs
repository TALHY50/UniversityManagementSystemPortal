using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Category;

namespace UniversityManagementSystemPortal.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class CategoriesController : ControllerBase
        {
            private readonly IMapper _mapper;
            private readonly ICategoryRepository _repository;

            public CategoriesController(IMapper mapper, ICategoryRepository repository)
            {
                _mapper = mapper;
                _repository = repository;
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<CategoryDto>> GetById(Guid id)
            {
                var category = await _repository.GetByIdAsync(id);

                if (category == null)
                {
                    return NotFound();
                }

                var categoryDto = _mapper.Map<CategoryDto>(category);

                return Ok(categoryDto);
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
            {
                var categories = await _repository.GetAllAsync();

                var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);

                return Ok(categoryDtos);
            }

            [HttpGet("{instituteId}/by-institute")]
            public async Task<ActionResult<IEnumerable<CategoryDto>>> GetByInstituteId(Guid instituteId)
            {
                var categories = await _repository.GetByInstituteIdAsync(instituteId);

                var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);

                return Ok(categoryDtos);
            }

            [HttpPost]
            public async Task<ActionResult<CategoryDto>> Create(CategoryCreateDto createCategoryDto)
            {
                var category = _mapper.Map<Category>(createCategoryDto);

                category.CreatedAt = DateTime.UtcNow;
                category.IsActive = true;

                category = await _repository.AddAsync(category);

                var categoryDto = _mapper.Map<CategoryDto>(category);

                return CreatedAtAction(nameof(GetById), new { id = category.Id }, categoryDto);
            }

            [HttpPut("{id}")]
            public async Task<ActionResult<CategoryDto>> Update(Guid id, CategoryUpdateDto updateCategoryDto)
            {
                var category = await _repository.GetByIdAsync(id);

                if (category == null)
                {
                    return NotFound();
                }

                _mapper.Map(updateCategoryDto, category);

                category.UpdatedAt = DateTime.UtcNow;

                category = await _repository.UpdateAsync(category);

                var categoryDto = _mapper.Map<CategoryDto>(category);

                return Ok(categoryDto);
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult> Delete(Guid id)
            {
                var category = await _repository.GetByIdAsync(id);

                if (category == null)
                {
                    return NotFound();
                }

                await _repository.DeleteAsync(category);

                return NoContent();
            }
        }
 }


