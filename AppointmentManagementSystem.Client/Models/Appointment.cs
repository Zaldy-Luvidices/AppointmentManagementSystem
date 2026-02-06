using System;

namespace AppointmentManagementSystem.Client.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public string PatientName = string.Empty;
        public string Title = string.Empty;
        public string Description = string.Empty;
        public DateTime ScheduledDate = DateTime.Now;
    }
}
