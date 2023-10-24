using API.DTOs;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class SupplierController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SupplierController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<Pager<SupplierDto>>> Get(
        [FromQuery] Params SupplierParams
    )
    {
        if (SupplierParams == null)
        {
            return BadRequest(new ApiResponse(400, "Params cannot be null"));
        }
        var (totalRegisters, registers) = await _unitOfWork.Supplier.GetAllAsync(
            SupplierParams.PageIndex,
            SupplierParams.PageSize
        );
        var SupplierListDto = _mapper.Map<List<SupplierDto>>(registers);
        return new Pager<SupplierDto>(
            SupplierListDto,
            totalRegisters,
            SupplierParams.PageIndex,
            SupplierParams.PageSize
        );
    }

    [HttpPost]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<Supplier>> Post(SupplierDto SupplierDto)
    {
        var Supplier = _mapper.Map<Supplier>(SupplierDto);
        if (Supplier == null)
        {
            return BadRequest(new ApiResponse(400, "Object cannot be null"));
        }
        _unitOfWork.Supplier.Add(Supplier);
        await _unitOfWork.SaveAsync();
        SupplierDto.Id = Supplier.Id;
        return CreatedAtAction(nameof(Post), new { id = SupplierDto.Id }, SupplierDto);
    }

    [HttpPut("{id}")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<SupplierDto>> Put(
        int id,
        [FromBody] SupplierDto SupplierDto
    )
    {
        if (SupplierDto == null)
        {
            return NotFound(new ApiResponse(404));
        }
        var Supplier = _mapper.Map<Supplier>(SupplierDto);
        _unitOfWork.Supplier.Update(Supplier);
        await _unitOfWork.SaveAsync();
        return SupplierDto;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult> Delete(int id)
    {
        var Supplier = await _unitOfWork.Supplier.GetByIdAsync(id);
        if (Supplier == null)
        {
            return BadRequest(new ApiResponse(400));
        }
        _unitOfWork.Supplier.Remove(Supplier);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    [HttpGet("WhoShells")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<Pager<SupplierDto>>> GetWhoSells(
        [FromQuery] Params SupplierParams,
        string medicineName
    )
    {
        if (SupplierParams == null)
        {
            return BadRequest(new ApiResponse(400, "Params cannot be null"));
        }
        var (registers, totalRegisters) = await _unitOfWork.Supplier.GetWhoSells(
            SupplierParams.PageIndex,
            SupplierParams.PageSize,
            medicineName
        );
        var SupplierListDto = _mapper.Map<List<SupplierDto>>(registers);
        return new Pager<SupplierDto>(
            SupplierListDto,
            totalRegisters,
            SupplierParams.PageIndex,
            SupplierParams.PageSize
        );
    }
}
