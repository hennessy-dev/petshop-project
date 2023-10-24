using API.DTOs;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Authorize(Roles = "Employee")]
public class PurchasedMedicineController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PurchasedMedicineController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<Pager<PurchasedMedicineDto>>> Get(
        [FromQuery] Params PurchasedMedicineParams
    )
    {
        if (PurchasedMedicineParams == null)
        {
            return BadRequest(new ApiResponse(400, "Params cannot be null"));
        }
        var (totalRegisters, registers) = await _unitOfWork.PurchasedMedicine.GetAllAsync(
            PurchasedMedicineParams.PageIndex,
            PurchasedMedicineParams.PageSize
        );
        var PurchasedMedicineListDto = _mapper.Map<List<PurchasedMedicineDto>>(registers);
        return new Pager<PurchasedMedicineDto>(
            PurchasedMedicineListDto,
            totalRegisters,
            PurchasedMedicineParams.PageIndex,
            PurchasedMedicineParams.PageSize
        );
    }

    [HttpPost]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<PurchasedMedicine>> Post(PurchasedMedicineDto PurchasedMedicineDto)
    {
        var PurchasedMedicine = _mapper.Map<PurchasedMedicine>(PurchasedMedicineDto);
        if (PurchasedMedicine == null)
        {
            return BadRequest(new ApiResponse(400, "Object cannot be null"));
        }
        await _unitOfWork.Medicine.AddMedicine(PurchasedMedicine.Id,PurchasedMedicine.Amount);
        _unitOfWork.PurchasedMedicine.Add(PurchasedMedicine);
        await _unitOfWork.SaveAsync();
        PurchasedMedicineDto.Id = PurchasedMedicine.Id;
        return CreatedAtAction(nameof(Post), new { id = PurchasedMedicineDto.Id }, PurchasedMedicineDto);
    }

    [HttpPut("{id}")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<PurchasedMedicineDto>> Put(
        int id,
        [FromBody] PurchasedMedicineDto PurchasedMedicineDto
    )
    {
        if (PurchasedMedicineDto == null)
        {
            return NotFound(new ApiResponse(404));
        }
        var PurchasedMedicine = _mapper.Map<PurchasedMedicine>(PurchasedMedicineDto);
        _unitOfWork.PurchasedMedicine.Update(PurchasedMedicine);
        await _unitOfWork.SaveAsync();
        return PurchasedMedicineDto;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult> Delete(int id)
    {
        var PurchasedMedicine = await _unitOfWork.PurchasedMedicine.GetByIdAsync(id);
        if (PurchasedMedicine == null)
        {
            return BadRequest(new ApiResponse(400));
        }
        _unitOfWork.PurchasedMedicine.Remove(PurchasedMedicine);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}