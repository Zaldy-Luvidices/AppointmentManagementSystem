using AppointmentManagementSystem.Client.Exceptions;
using AppointmentManagementSystem.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
            try
            {
                var response = await _httpClient.GetAsync("api/appointment");
                if (!response.IsSuccessStatusCode) await ThrowFailedStatusCodeException(response);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Appointment>>(json) ?? new List<Appointment>();
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("Network error", ex);
            }
        }

        public async Task CreateAppointmentAsync(Appointment appointment)
        {
            try
            {
                var json = JsonConvert.SerializeObject(appointment);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/appointment", content);
                if (!response.IsSuccessStatusCode) await ThrowFailedStatusCodeException(response);
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("Network error", ex);
            }
        }

        public async Task UpdateAppointmentAsync(Guid id, Appointment appointmentDetails)
        {
            try
            {
                var json = JsonConvert.SerializeObject(appointmentDetails);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/appointment/{id}", content);
                if (!response.IsSuccessStatusCode) await ThrowFailedStatusCodeException(response);
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("Network error", ex);
            }
        }

        public async Task DeleteAppointmentAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/appointment/{id}");
                if (!response.IsSuccessStatusCode) await ThrowFailedStatusCodeException(response);
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("Network error", ex);
            }
        }

        private async Task ThrowFailedStatusCodeException(HttpResponseMessage response)
        {
            var message = await response.Content.ReadAsStringAsync();
            throw new ApiException(response.StatusCode, message);
        }
    }
}
