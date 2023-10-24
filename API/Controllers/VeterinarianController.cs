using API.DTOs;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Authorize(Roles = "Employee")]
public class VeterinarianController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public VeterinarianController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<Pager<VeterinarianDto>>> Get(
        [FromQuery] Params VeterinarianParams
    )   
    {
        if (VeterinarianParams == null)
        {
            return BadRequest(new ApiResponse(400, "Params cannot be null"));
        }
        var (totalRegisters, registers) = await _unitOfWork.Veterinarian.GetAllAsync(
            VeterinarianParams.PageIndex,
            VeterinarianParams.PageSize
        );
        var VeterinarianListDto = _mapper.Map<List<VeterinarianDto>>(registers);
        return new Pager<VeterinarianDto>(
            VeterinarianListDto,
            totalRegisters,
            VeterinarianParams.PageIndex,
            VeterinarianParams.PageSize
        );
    }

    [HttpPost]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<Veterinarian>> Post(VeterinarianDto VeterinarianDto)
    {
        var Veterinarian = _mapper.Map<Veterinarian>(VeterinarianDto);
        if (Veterinarian == null)
        {
            return BadRequest(new ApiResponse(400, "Object cannot be null"));
        }
        _unitOfWork.Veterinarian.Add(Veterinarian);
        await _unitOfWork.SaveAsync();
        VeterinarianDto.Id = Veterinarian.Id;
        return CreatedAtAction(nameof(Post), new { id = VeterinarianDto.Id }, VeterinarianDto);
    }

    [HttpPut("{id}")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<VeterinarianDto>> Put(
        int id,
        [FromBody] VeterinarianDto VeterinarianDto
    )
    {
        if (VeterinarianDto == null)
        {
            return NotFound(new ApiResponse(404));
        }
        var Veterinarian = _mapper.Map<Veterinarian>(VeterinarianDto);
        _unitOfWork.Veterinarian.Update(Veterinarian);
        await _unitOfWork.SaveAsync();
        return VeterinarianDto;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult> Delete(int id)
    {
        var Veterinarian = await _unitOfWork.Veterinarian.GetByIdAsync(id);
        if (Veterinarian == null)
        {
            return BadRequest(new ApiResponse(400));
        }
        _unitOfWork.Veterinarian.Remove(Veterinarian);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    [HttpGet("speciality")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<Pager<VeterinarianDto>>> Get(
        [FromQuery] Params VeterinarianParams,
        string speciality
    )
    {
        if (VeterinarianParams == null || speciality == null)
        {
            return BadRequest(new ApiResponse(400, "Params or speciality cannot be null"));
        }
        var (registers,totalRegisters) = await _unitOfWork.Veterinarian.GetVeterinariansBySpeciality(
            VeterinarianParams.PageIndex,
            VeterinarianParams.PageSize,
            speciality
        );
        var VeterinarianListDto = _mapper.Map<List<VeterinarianDto>>(registers);
        return new Pager<VeterinarianDto>(
            VeterinarianListDto,
            totalRegisters,
            VeterinarianParams.PageIndex,
            VeterinarianParams.PageSize
        );
    }
}