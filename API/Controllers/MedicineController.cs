using API.DTOs;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;
[Authorize(Roles = "Employee")]
public class MedicineController : BaseApiController
{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicineController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<MedicineDto>>> Get(
            [FromQuery] Params MedicineParams
        )
        {
            if (MedicineParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (totalRegisters, registers) = await _unitOfWork.Medicine.GetAllAsync(
                MedicineParams.PageIndex,
                MedicineParams.PageSize
            );
            var MedicineListDto = _mapper.Map<List<MedicineDto>>(registers);
            return new Pager<MedicineDto>(
                MedicineListDto,
                totalRegisters,
                MedicineParams.PageIndex,
                MedicineParams.PageSize
            );
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Medicine>> Post(MedicineDto MedicineDto)
        {
            var Medicine = _mapper.Map<Medicine>(MedicineDto);
            if (Medicine == null)
            {
                return BadRequest(new ApiResponse(400, "Object cannot be null"));
            }
            _unitOfWork.Medicine.Add(Medicine);
            await _unitOfWork.SaveAsync();
            MedicineDto.Id = Medicine.Id;
            return CreatedAtAction(nameof(Post), new { id = MedicineDto.Id }, MedicineDto);
        }

        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<MedicineDto>> Put(
            int id,
            [FromBody] MedicineDto MedicineDto
        )
        {
            if (MedicineDto == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var Medicine = _mapper.Map<Medicine>(MedicineDto);
            _unitOfWork.Medicine.Update(Medicine);
            await _unitOfWork.SaveAsync();
            return MedicineDto;
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete(int id)
        {
            var Medicine = await _unitOfWork.Medicine.GetByIdAsync(id);
            if (Medicine == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            _unitOfWork.Medicine.Remove(Medicine);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
        [HttpGet("laboratory")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<MedicineDto>>> Get(
            [FromQuery] Params MedicineParams,
            string laboratory
        )
        {
            if (MedicineParams == null || laboratory == null)
            {
                return BadRequest(new ApiResponse(400, "Params or laboratory cannot be null"));
            }
            var (registers,totalRegisters) = await _unitOfWork.Medicine.GetMedicinesByLaboratory(
                MedicineParams.PageIndex,
                MedicineParams.PageSize,
                laboratory
            );
            var MedicineListDto = _mapper.Map<List<MedicineDto>>(registers);
            return new Pager<MedicineDto>(
                MedicineListDto,
                totalRegisters,
                MedicineParams.PageIndex,
                MedicineParams.PageSize
            );
        }
        [HttpGet("ExpensiveThan50k")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<MedicineDto>>> GetMt(
            [FromQuery] Params MedicineParams
        )
        {
            if (MedicineParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (registers, totalRegisters) = await _unitOfWork.Medicine.GetMedicinesExpensiveThan(
                MedicineParams.PageIndex,
                MedicineParams.PageSize
            );
            var MedicineListDto = _mapper.Map<List<MedicineDto>>(registers);
            return new Pager<MedicineDto>(
                MedicineListDto,
                totalRegisters,
                MedicineParams.PageIndex,
                MedicineParams.PageSize
            );
        }
    }