using Microsoft.AspNetCore.Mvc;
using APBDtut6._1.DTO;
using APBDtut6._1.Appointment;
using APBDtut6._1.Models;

namespace ClinicApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppointmentListDto>>> GetAppointments([FromQuery] string? status, [FromQuery] string? patientLastName)
    {
        var appointments = await _appointmentService.GetAppointmentsAsync(status, patientLastName);
        return Ok(appointments);
    }

    [HttpGet("{idAppointment}")]
    public async Task<ActionResult<AppointmentDetailsDto>> GetAppointment(int idAppointment)
    {
        var result = await _appointmentService.GetAppointmentAsync(idAppointment);
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentRequestDto request)
    {
        var result = await _appointmentService.CreateAppointmentAsync(request);
        if (result.IsSuccess)
        {
            return CreatedAtAction(nameof(GetAppointment), new { idAppointment = result.Data }, request);
        }
        return HandleResult(result);
    }

    [HttpPut("{idAppointment}")]
    public async Task<IActionResult> UpdateAppointment(int idAppointment, [FromBody] UpdateAppointmentRequestDto request)
    {
        var result = await _appointmentService.UpdateAppointmentAsync(idAppointment, request);
        return HandleResult(result, isUpdateOrDelete: true);
    }

    [HttpDelete("{idAppointment}")]
    public async Task<IActionResult> DeleteAppointment(int idAppointment)
    {
        var result = await _appointmentService.DeleteAppointmentAsync(idAppointment);
        return HandleResult(result, isUpdateOrDelete: true);
    }
    private ActionResult HandleResult(ServiceResult result, bool isUpdateOrDelete = false)
    {
        if (result.IsSuccess) return isUpdateOrDelete ? NoContent() : Ok();

        var errorResponse = new ErrorResponseDto { Message = result.ErrorMessage };
        return result.ErrorTypes switch
        {
            ErrorTypes.NotFound => NotFound(errorResponse),
            ErrorTypes.BadRequest => BadRequest(errorResponse),
            ErrorTypes.Conflict => Conflict(errorResponse),
            _ => StatusCode(500, errorResponse)
        };
    }

    private ActionResult HandleResult<T>(ServiceResult<T> result)
    {
        if (result.IsSuccess) return Ok(result.Data);
        return HandleResult((ServiceResult)result);
    }
}