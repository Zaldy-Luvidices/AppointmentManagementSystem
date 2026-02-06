using AppointmentManagementSystem.Api.Models;
using AppointmentManagementSystem.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentManagementSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;
    private readonly ILogger<AppointmentController> _logger;

    public AppointmentController(
        IAppointmentService appointmentService,
        ILogger<AppointmentController> logger)
    {
        _appointmentService = appointmentService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<Appointment>>> GetAll()
    {
        try
        {
            _logger.LogInformation("Getting all appointments...");
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting appointments");
            return StatusCode(500, "An error occurred while getting appointments.");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create(Appointment appointment)
    {
        try
        {
            if (appointment == null) return BadRequest("Appointment cannot be null");
            _logger.LogInformation("Creating appointment for {Patient}...", appointment.PatientName);
            if (appointment.Id == Guid.Empty) appointment.Id = Guid.NewGuid();

            await _appointmentService.AddAppointmentAsync(appointment);
            return CreatedAtAction(nameof(Create), new { id = appointment.Id }, appointment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating appointment for {Patient}", appointment?.PatientName);
            return StatusCode(500, "An error occurred while creating the appointment.");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, Appointment appointmentDetails)
    {
        try
        {
            if (appointmentDetails == null) return BadRequest("Invalid appointment data");
            var existing = await _appointmentService.GetAppointmentAsync(id);
            if (existing == null) return NotFound();

            _logger.LogInformation("Updating appointment {Id}...", id);
            await _appointmentService.UpdateAppointmentAsync(existing, appointmentDetails);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating appointment {Id}", id);
            return StatusCode(500, "An error occurred while updating the appointment.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            var existing = await _appointmentService.GetAppointmentAsync(id);
            if (existing == null) return NotFound();

            _logger.LogInformation("Deleting appointment {Id}...", id);
            await _appointmentService.DeleteAppointmentAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting appointment {Id}", id);
            return StatusCode(500, "An error occurred while deleting the appointment.");
        }
    }
}
