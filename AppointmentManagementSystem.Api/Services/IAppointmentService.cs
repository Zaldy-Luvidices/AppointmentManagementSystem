using AppointmentManagementSystem.Api.Models;

namespace AppointmentManagementSystem.Api.Services;

public interface IAppointmentService
{
    Task<List<Appointment>> GetAllAppointmentsAsync();
    Task<Appointment?> GetAppointmentAsync(Guid id);
    Task AddAppointmentAsync(Appointment appointment);
    Task UpdateAppointmentAsync(Appointment existingAppointment, Appointment appointmentDetails);
    Task DeleteAppointmentAsync(Guid id);
}
