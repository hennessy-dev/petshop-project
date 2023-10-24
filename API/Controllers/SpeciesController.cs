using API.DTOs;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Employee")]
    public class SpeciesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SpeciesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<SpeciesDto>>> Get(
            [FromQuery] Params SpeciesParams
        )
        {
            if (SpeciesParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (totalRegisters, registers) = await _unitOfWork.Species.GetAllAsync(
                SpeciesParams.PageIndex,
                SpeciesParams.PageSize
            );
            var SpeciesListDto = _mapper.Map<List<SpeciesDto>>(registers);
            return new Pager<SpeciesDto>(
                SpeciesListDto,
                totalRegisters,
                SpeciesParams.PageIndex,
                SpeciesParams.PageSize
            );
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Specie>> Post(SpeciesDto SpeciesDto)
        {
            var Species = _mapper.Map<Specie>(SpeciesDto);
            if (Species == null)
            {
                return BadRequest(new ApiResponse(400, "Object cannot be null"));
            }
            _unitOfWork.Species.Add(Species);
            await _unitOfWork.SaveAsync();
            SpeciesDto.Id = Species.Id;
            return CreatedAtAction(nameof(Post), new { id = SpeciesDto.Id }, SpeciesDto);
        }

        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<SpeciesDto>> Put(
            int id,
            [FromBody] SpeciesDto SpeciesDto
        )
        {
            if (SpeciesDto == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var Species = _mapper.Map<Specie>(SpeciesDto);
            _unitOfWork.Species.Update(Species);
            await _unitOfWork.SaveAsync();
            return SpeciesDto;
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete(int id)
        {
            var Species = await _unitOfWork.Species.GetByIdAsync(id);
            if (Species == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            _unitOfWork.Species.Remove(Species);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}