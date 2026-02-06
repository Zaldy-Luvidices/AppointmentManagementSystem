using AppointmentManagementSystem.Api.Models;

namespace AppointmentManagementSystem.Api.Repositories;

public interface IAppointmentRepository
{
    Task<List<Appointment>> GetAllAsync();
    Task<Appointment?> GetAsync(Guid id);
    Task AddAsync(Appointment appointment);
    Task UpdateAsync(Appointment appointment);
    Task DeleteAsync(Guid id);
}
