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
    public class SoldMedicineController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SoldMedicineController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<SoldMedicineDto>>> Get(
            [FromQuery] Params SoldMedicineParams
        )
        {
            if (SoldMedicineParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (totalRegisters, registers) = await _unitOfWork.SoldMedicine.GetAllAsync(
                SoldMedicineParams.PageIndex,
                SoldMedicineParams.PageSize
            );
            var SoldMedicineListDto = _mapper.Map<List<SoldMedicineDto>>(registers);
            return new Pager<SoldMedicineDto>(
                SoldMedicineListDto,
                totalRegisters,
                SoldMedicineParams.PageIndex,
                SoldMedicineParams.PageSize
            );
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<SoldMedicine>> Post(SoldMedicineDto SoldMedicineDto)
        {
            var SoldMedicine = _mapper.Map<SoldMedicine>(SoldMedicineDto);
            if (SoldMedicine == null)
            {
                return BadRequest(new ApiResponse(400, "Object cannot be null"));
            }
            await _unitOfWork.Medicine.RestMedicine(SoldMedicineDto.MedicineId,SoldMedicineDto.Amount);
            _unitOfWork.SoldMedicine.Add(SoldMedicine);
            await _unitOfWork.SaveAsync();
            SoldMedicineDto.Id = SoldMedicine.Id;
            return CreatedAtAction(nameof(Post), new { id = SoldMedicineDto.Id }, SoldMedicineDto);
        }

        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<SoldMedicineDto>> Put(
            int id,
            [FromBody] SoldMedicineDto SoldMedicineDto
        )
        {
            if (SoldMedicineDto == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var SoldMedicine = _mapper.Map<SoldMedicine>(SoldMedicineDto);
            _unitOfWork.SoldMedicine.Update(SoldMedicine);
            await _unitOfWork.SaveAsync();
            return SoldMedicineDto;
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete(int id)
        {
            var SoldMedicine = await _unitOfWork.SoldMedicine.GetByIdAsync(id);
            if (SoldMedicine == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            _unitOfWork.SoldMedicine.Remove(SoldMedicine);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}