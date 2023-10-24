using API.DTOs;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Authorize(Roles = "Employee")]
public class MedicalTreatmentController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicalTreatmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<MedicalTreatmentDto>>> Get(
            [FromQuery] Params MedicalTreatmentParams
        )
        {
            if (MedicalTreatmentParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (totalRegisters, registers) = await _unitOfWork.MedicalTreatment.GetAllAsync(
                MedicalTreatmentParams.PageIndex,
                MedicalTreatmentParams.PageSize
            );
            var MedicalTreatmentListDto = _mapper.Map<List<MedicalTreatmentDto>>(registers);
            return new Pager<MedicalTreatmentDto>(
                MedicalTreatmentListDto,
                totalRegisters,
                MedicalTreatmentParams.PageIndex,
                MedicalTreatmentParams.PageSize
            );
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<MedicalTreatment>> Post(MedicalTreatmentDto MedicalTreatmentDto)
        {
            var MedicalTreatment = _mapper.Map<MedicalTreatment>(MedicalTreatmentDto);
            if (MedicalTreatment == null)
            {
                return BadRequest(new ApiResponse(400, "Object cannot be null"));
            }
            _unitOfWork.MedicalTreatment.Add(MedicalTreatment);
            await _unitOfWork.SaveAsync();
            MedicalTreatmentDto.Id = MedicalTreatment.Id;
            return CreatedAtAction(nameof(Post), new { id = MedicalTreatmentDto.Id }, MedicalTreatmentDto);
        }

        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<MedicalTreatmentDto>> Put(
            int id,
            [FromBody] MedicalTreatmentDto MedicalTreatmentDto
        )
        {
            if (MedicalTreatmentDto == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var MedicalTreatment = _mapper.Map<MedicalTreatment>(MedicalTreatmentDto);
            _unitOfWork.MedicalTreatment.Update(MedicalTreatment);
            await _unitOfWork.SaveAsync();
            return MedicalTreatmentDto;
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete(int id)
        {
            var MedicalTreatment = await _unitOfWork.MedicalTreatment.GetByIdAsync(id);
            if (MedicalTreatment == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            _unitOfWork.MedicalTreatment.Remove(MedicalTreatment);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
}