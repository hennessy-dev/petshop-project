using API.DTOs;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiVersion("1.0")]
[ApiVersion("1.1")]
[Authorize(Roles = "Employee")]
public class AppointmentController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AppointmentController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<Pager<AppointmentDto>>> Get(
        [FromQuery] Params AppointmentParams
    )
    {
        if (AppointmentParams == null)
        {
            return BadRequest(new ApiResponse(400, "Params can't be null"));
        }
        var (totalRegisters, registers) = await _unitOfWork.Appointment.GetAllAsync(
            AppointmentParams.PageIndex,
            AppointmentParams.PageSize
        );
        var AppointmentListDto = _mapper.Map<List<AppointmentDto>>(registers);
        return new Pager<AppointmentDto>(
            AppointmentListDto,
            totalRegisters,
            AppointmentParams.PageIndex,
            AppointmentParams.PageSize
        );
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    public async Task<ActionResult<IEnumerable<AppointmentDto>>> Get1_1()
    {
        var registers = await _unitOfWork.Appointment.GetAllAsync();
        var AppointmentListDto = _mapper.Map<List<AppointmentDto>>(registers);
        return AppointmentListDto;
    }

    [HttpPost]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<Appointment>> Post(AppointmentDto AppointmentDto)
    {
        var Appointment = _mapper.Map<Appointment>(AppointmentDto);
        if (Appointment == null)
        {
            return BadRequest(new ApiResponse(400, "Object cannot be null"));
        }
        _unitOfWork.Appointment.Add(Appointment);
        await _unitOfWork.SaveAsync();
        AppointmentDto.Id = Appointment.Id;
        return CreatedAtAction(nameof(Post), new { id = AppointmentDto.Id }, AppointmentDto);
    }

    [HttpPut("{id}")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<AppointmentDto>> Put(
        int id,
        [FromBody] AppointmentDto AppointmentDto
    )
    {
        if (AppointmentDto == null)
        {
            return NotFound(new ApiResponse(404));
        }
        var Appointment = _mapper.Map<Appointment>(AppointmentDto);
        _unitOfWork.Appointment.Update(Appointment);
        await _unitOfWork.SaveAsync();
        return AppointmentDto;
    }

    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult> Delete(int id)
    {
        var Appointment = await _unitOfWork.Appointment.GetByIdAsync(id);
        if (Appointment == null)
        {
            return BadRequest(new ApiResponse(400));
        }
        _unitOfWork.Appointment.Remove(Appointment);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
