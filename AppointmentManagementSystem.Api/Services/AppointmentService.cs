using AppointmentManagementSystem.Api.Models;
using AppointmentManagementSystem.Api.Repositories;

namespace AppointmentManagementSystem.Api.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repository;

    public AppointmentService(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Appointment>> GetAllAppointmentsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Appointment?> GetAppointmentAsync(Guid id)
    {
        return await _repository.GetAsync(id);
    }

    public async Task AddAppointmentAsync(Appointment appointment)
    {
        await _repository.AddAsync(appointment);
    }

    public async Task UpdateAppointmentAsync(Appointment existingAppointment, Appointment appointmentDetails)
    {
        existingAppointment.Title = appointmentDetails.Title;
        existingAppointment.PatientName = appointmentDetails.PatientName;
        existingAppointment.Description = appointmentDetails.Description;
        existingAppointment.ScheduledDate = appointmentDetails.ScheduledDate;
        await _repository.UpdateAsync(existingAppointment);
    }

    public async Task DeleteAppointmentAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}
