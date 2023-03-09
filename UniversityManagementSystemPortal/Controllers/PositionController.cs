using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UniversityManagementSystemPortal.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using global::AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using UniversityManagementsystem.Models;
    using UniversityManagementSystemPortal.Authorization.UniversityManagementSystemPortal.Authorization;
    using UniversityManagementSystemPortal.Interfaces;
    using UniversityManagementSystemPortal.ModelDto.Position;
  

    namespace YourApplicationName.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class PositionController : ControllerBase
        {
            private readonly IPositionRepository _repository;
            private readonly IMapper _mapper;

            public PositionController(IPositionRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }
            [JwtAuthorize("Admin", "SuperAdmin")]
            [HttpGet]
            public async Task<ActionResult<IEnumerable<PositionDto>>> GetAll()
            {
                var positions = await _repository.GetAllAsync();

                var positionDtos = _mapper.Map<IEnumerable<PositionDto>>(positions);

                return Ok(positionDtos);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<PositionDto>> GetById(Guid id)
            {
                var position = await _repository.GetByIdAsync(id);

                if (position == null)
                {
                    return NotFound();
                }

                var positionDto = _mapper.Map<PositionDto>(position);

                return Ok(positionDto);
            }

            [HttpPost]
            public async Task<ActionResult<PositionDto>> Create(PositionAddorUpdateDto positionAddDto)
            {
                var position = _mapper.Map<Position>(positionAddDto);

                await _repository.CreateAsync(position);

                var positionDto = _mapper.Map<PositionDto>(position);

                return CreatedAtAction(nameof(GetById), new { id = positionDto.Id }, positionDto);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(Guid id, PositionAddorUpdateDto positionUpdateDto)
            {
                var position = await _repository.GetByIdAsync(id);

                if (position == null)
                {
                    return NotFound();
                }

                _mapper.Map(positionUpdateDto, position);

                await _repository.UpdateAsync(position);

                return NoContent();
            }
        }
    }

}
