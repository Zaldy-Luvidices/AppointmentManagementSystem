using AppointmentManagementSystem.Api.Models;
using AppointmentManagementSystem.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Appointment>>> GetAll()
    {
        var appointments = await _appointmentService.GetAllAppointmentsAsync();
        return Ok(appointments);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Appointment appointment)
    {
        if (appointment == null) return BadRequest("Appointment cannot be null");
        if (appointment.Id == Guid.Empty) appointment.Id = Guid.NewGuid();

        await _appointmentService.AddAppointmentAsync(appointment);
        return CreatedAtAction(nameof(Create), new { id = appointment.Id }, appointment);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, Appointment appointmentDetails)
    {
        if (appointmentDetails == null) return BadRequest("Invalid appointment data");
        var existing = await _appointmentService.GetAppointmentAsync(id);
        if (existing == null) return NotFound();

        await _appointmentService.UpdateAppointmentAsync(existing, appointmentDetails);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var existing = await _appointmentService.GetAppointmentAsync(id);
        if (existing == null) return NotFound();

        await _appointmentService.DeleteAppointmentAsync(id);
        return NoContent();
    }
}
