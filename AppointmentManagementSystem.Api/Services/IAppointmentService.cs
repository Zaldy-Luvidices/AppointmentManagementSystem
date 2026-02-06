using AppointmentManagementSystem.Api.Models;

namespace AppointmentManagementSystem.Api.Services;

public interface IAppointmentService
{
    Task<List<Appointment>> GetAllAppointmentsAsync();
    Task AddAppointmentAsync(Appointment appointment);
    Task UpdateAppointmentAsync(Appointment existingAppointment, Appointment appointmentDetails);
    Task DeleteAppointmentAsync(Guid id);
}
