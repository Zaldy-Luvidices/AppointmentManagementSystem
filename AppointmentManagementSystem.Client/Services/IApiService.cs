using AppointmentManagementSystem.Client.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Client.Services
{
    public interface IApiService
    {
        Task<List<Appointment>> GetAppointmentsAsync();
        Task CreateAppointmentAsync(Appointment appointment);
        Task UpdateAppointmentAsync(Guid id, Appointment appointmentDetails);
        Task DeleteAppointmentAsync(Guid id);
    }
}
