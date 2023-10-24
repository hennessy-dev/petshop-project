using API.DTOs;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Roles = "Employee")]
public class BreedController : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BreedController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<Pager<BreedDto>>> Get(
        [FromQuery] Params BreedParams
    )
    {
        if (BreedParams == null)
        {
            return BadRequest(new ApiResponse(400, "Params cannot be null"));
        }
        var (totalRegisters, registers) = await _unitOfWork.Breed.GetAllAsync(
            BreedParams.PageIndex,
            BreedParams.PageSize
        );
        var BreedListDto = _mapper.Map<List<BreedDto>>(registers);
        return new Pager<BreedDto>(
            BreedListDto,
            totalRegisters,
            BreedParams.PageIndex,
            BreedParams.PageSize
        );
    }

    [HttpPost]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<BreedDto>> Post(BreedDto BreedDto)
    {
        var Breed = _mapper.Map<Breed>(BreedDto);
        if (Breed == null)
        {
            return BadRequest(new ApiResponse(400, "Object cannot be null"));
        }
        _unitOfWork.Breed.Add(Breed);
        await _unitOfWork.SaveAsync();
        BreedDto.Id = Breed.Id;
        return CreatedAtAction(nameof(Post), new { id = BreedDto.Id }, BreedDto);
    }

    [HttpPut("{id}")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<BreedDto>> Put(
        int id,
        [FromBody] BreedDto BreedDto
    )
    {
        if (BreedDto == null)
        {
            return NotFound(new ApiResponse(404));
        }
        var Breed = _mapper.Map<Breed>(BreedDto);
        _unitOfWork.Breed.Update(Breed);
        await _unitOfWork.SaveAsync();
        return BreedDto;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult> Delete(int id)
    {
        var Breed = await _unitOfWork.Breed.GetByIdAsync(id);
        if (Breed == null)
        {
            return BadRequest(new ApiResponse(400));
        }
        _unitOfWork.Breed.Remove(Breed);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    [HttpGet("BreedWithPets")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<Pager<BreedWithManyPetsDto>>> GetWP(
        [FromQuery] Params BreedParams
    )
    {
        if (BreedParams == null)
        {
            return BadRequest(new ApiResponse(400, "Params cannot be null"));
        }
        var (totalRegisters, registers) = await _unitOfWork.Breed.GetHowManyPetsAreInTheBreed(
            BreedParams.PageIndex,
            BreedParams.PageSize
        );
        List<BreedWithManyPetsDto> Fregisters = new ();
        foreach (var r in totalRegisters){  
            var bw = new BreedWithManyPetsDto
                {
                    Breed = _mapper.Map<BreedDto>(r),
                    PetIds = r.Pets.Select(p=>p.Id),
                    Total = r.Pets.Select(p=>p.Id).Count()
                };
                Fregisters.Add(bw);
        }
        return new Pager<BreedWithManyPetsDto>(
            Fregisters,
            registers,
            BreedParams.PageIndex,
            BreedParams.PageSize
        );
    }
}