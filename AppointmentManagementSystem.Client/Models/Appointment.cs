using System;
using System.ComponentModel;

namespace AppointmentManagementSystem.Client.Models
{
    public class Appointment : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Guid Id { get; set; }

        private string _patientName = string.Empty;
        public string PatientName
        {
            get => _patientName;
            set { _patientName = value; OnPropertyChanged(nameof(PatientName)); }
        }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(nameof(Title)); }
        }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }

        private DateTime _scheduledDate = DateTime.Now;
        public DateTime ScheduledDate
        {
            get => _scheduledDate;
            set { _scheduledDate = value; OnPropertyChanged(nameof(ScheduledDate)); }
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
