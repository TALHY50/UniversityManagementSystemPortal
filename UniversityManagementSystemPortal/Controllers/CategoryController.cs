using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementSystemPortal.Application.Command.Category;
using UniversityManagementSystemPortal.Application.Qurey.Category;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Category;

namespace UniversityManagementSystemPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(Guid id)
        {
            var categoryDto = await _mediator.Send(new GetCategoryByIdQuery { Id = id });
            return Ok(categoryDto);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
        {
            var categoryDtos = await _mediator.Send(new GetAllCategoriesQuery());
            return Ok(categoryDtos);
        }

        [HttpGet("{instituteId}/by-institute")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetByInstituteId(Guid instituteId)
        {
            var categoryDtos = await _mediator.Send(new GetCategoriesByInstituteIdQuery { InstituteId = instituteId });
            return Ok(categoryDtos);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create(CategoryCreateDto createCategoryDto)
        {
            var categoryDto = await _mediator.Send(new CreateCategoryCommand { CreateCategoryDto = createCategoryDto });
            return CreatedAtAction(nameof(GetById), new { id = categoryDto.Id }, categoryDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> Update(Guid id, CategoryUpdateDto updateCategoryDto)
        {
            var command = new UpdateCategoryCommand { Id = id, UpdateCategoryDto = updateCategoryDto };
            var categoryDto = await _mediator.Send(command);
            return Ok(categoryDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteCategoryCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}


