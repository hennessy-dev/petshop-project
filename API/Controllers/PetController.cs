using API.DTOs;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Authorize(Roles = "Employee")]
    public class PetController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PetController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<PetDto>>> Get(
            [FromQuery] Params PetParams
        )
        {
            if (PetParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (totalRegisters, registers) = await _unitOfWork.Pet.GetAllAsync(
                PetParams.PageIndex,
                PetParams.PageSize
            );
            var PetListDto = _mapper.Map<List<PetDto>>(registers);
            return new Pager<PetDto>(
                PetListDto,
                totalRegisters,
                PetParams.PageIndex,
                PetParams.PageSize
            );
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pet>> Post(PetDto PetDto)
        {
            var Pet = _mapper.Map<Pet>(PetDto);
            if (Pet == null)
            {
                return BadRequest(new ApiResponse(400, "Object cannot be null"));
            }
            _unitOfWork.Pet.Add(Pet);
            await _unitOfWork.SaveAsync();
            PetDto.Id = Pet.Id;
            return CreatedAtAction(nameof(Post), new { id = PetDto.Id }, PetDto);
        }

        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<PetDto>> Put(
            int id,
            [FromBody] PetDto PetDto
        )
        {
            if (PetDto == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var Pet = _mapper.Map<Pet>(PetDto);
            _unitOfWork.Pet.Update(Pet);
            await _unitOfWork.SaveAsync();
            return PetDto;
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete(int id)
        {
            var Pet = await _unitOfWork.Pet.GetByIdAsync(id);
            if (Pet == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            _unitOfWork.Pet.Remove(Pet);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
        [HttpGet("species")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<PetDto>>> Get(
            [FromQuery] Params PetParams,
            string Species
        )
        {
            if (PetParams == null || Species == null)
            {
                return BadRequest(new ApiResponse(400, "Params or species cannot be null"));
            }
            var (registers,totalRegisters) = await _unitOfWork.Pet.GetPetsBySpecies(
                PetParams.PageIndex,
                PetParams.PageSize,
                Species
            );
            var PetListDto = _mapper.Map<List<PetDto>>(registers);
            return new Pager<PetDto>(
                PetListDto,
                totalRegisters,
                PetParams.PageIndex,
                PetParams.PageSize
            );
        }
        [HttpGet("appointmentReason")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<PetDto>>> GetByReason(
            [FromQuery] Params PetParams,
            string Reason
        )
        {
            if (PetParams == null || Reason == null)
            {
                return BadRequest(new ApiResponse(400, "Params or appointmentReason cannot be null"));
            }
            var (registers,totalRegisters) = await _unitOfWork.Pet.GetPetsByAppointmentReason(
                PetParams.PageIndex,
                PetParams.PageSize,
                Reason
            );
            var PetListDto = _mapper.Map<List<PetDto>>(registers);
            return new Pager<PetDto>(
                PetListDto,
                totalRegisters,
                PetParams.PageIndex,
                PetParams.PageSize
            );
        }
        [HttpGet("veterinarian")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<PetDto>>> GetByVeterinarian(
            [FromQuery] Params PetParams,
            string VeterinarianName
        )
        {
            if (PetParams == null || VeterinarianName == null)
            {
                return BadRequest(new ApiResponse(400, "Params or Veterinarian cannot be null"));
            }
            var (registers,totalRegisters) = await _unitOfWork.Pet.GetPetsByVeterinarian(
                PetParams.PageIndex,
                PetParams.PageSize,
                VeterinarianName
            );
            var PetListDto = _mapper.Map<List<PetDto>>(registers);
            return new Pager<PetDto>(
                PetListDto,
                totalRegisters,
                PetParams.PageIndex,
                PetParams.PageSize
            );
        }
        [HttpGet("breed")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<PetWithOwnerDto>>> GetByBreed(
            [FromQuery] Params PetParams,
            string Breed
        )
        {
            if (PetParams == null || Breed == null)
            {
                return BadRequest(new ApiResponse(400, "Params or Breed cannot be null"));
            }
            var (registers,totalRegisters) = await _unitOfWork.Pet.GetPetsByBreed(
                PetParams.PageIndex,
                PetParams.PageSize,
                Breed
            );
            var PetListDto = _mapper.Map<List<PetWithOwnerDto>>(registers);
            return new Pager<PetWithOwnerDto>(
                PetListDto,
                totalRegisters,
                PetParams.PageIndex,
                PetParams.PageSize
            );
        }
    }
}