using AppointmentManagementSystem.Client.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AppointmentManagementSystem.Client.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Appointment>> GetAppointmentsAsync()
        {
            var response = await _httpClient.GetAsync("api/appointments");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Appointment>>(json) ?? new List<Appointment>();
        }

        public async Task CreateAppointmentAsync(Appointment appointment)
        {
            var json = JsonConvert.SerializeObject(appointment);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/appointments", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAppointmentAsync(Guid id, Appointment appointmentDetails)
        {
            var json = JsonConvert.SerializeObject(appointmentDetails);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/appointments/{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAppointmentAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/appointments/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
