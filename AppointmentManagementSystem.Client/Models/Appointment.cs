using System;

namespace AppointmentManagementSystem.Client.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        private string PatientName = string.Empty;
        private string Title = string.Empty;
        private string Description = string.Empty;
        private DateTime ScheduledDate = DateTime.Now;
    }
}
