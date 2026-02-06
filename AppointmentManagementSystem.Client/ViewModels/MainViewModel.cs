using AppointmentManagementSystem.Client.Commands;
using AppointmentManagementSystem.Client.Models;
using AppointmentManagementSystem.Client.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppointmentManagementSystem.Client.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IApiService _apiService;

        private ObservableCollection<Appointment> _appointments = new ObservableCollection<Appointment>();
        public ObservableCollection<Appointment> Appointments
        {
            get => _appointments;
            set
            {
                _appointments = value;
                OnPropertyChanged(nameof(Appointments));
            }
        }

        private Appointment _inputAppointment = new Appointment();
        public Appointment InputAppointment
        {
            get => _inputAppointment;
            set
            {
                _inputAppointment = value;
                OnPropertyChanged(nameof(InputAppointment));
            }
        }

        private Appointment _selectedAppointment;
        public Appointment SelectedAppointment
        {
            get => _selectedAppointment;
            set
            {
                _selectedAppointment = value;
                OnPropertyChanged(nameof(SelectedAppointment));
                if (_selectedAppointment != null)
                {
                    InputAppointment.Title = _selectedAppointment.Title;
                    InputAppointment.Description = _selectedAppointment.Description;
                    InputAppointment.PatientName = _selectedAppointment.PatientName;
                    InputAppointment.ScheduledDate = _selectedAppointment.ScheduledDate;
                }
                else ResetInputForm();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }

        public MainViewModel(IApiService apiService)
        {
            _apiService = apiService;

            AddCommand = new RelayCommand(async () => await AddAppointment());
            UpdateCommand = new RelayCommand(async () => await UpdateAppointment());
            _ = LoadAppointments();
        }

        public async Task LoadAppointments()
        {
            var appointments = await _apiService.GetAppointmentsAsync();
            Appointments.Clear();
            foreach (var appt in appointments) Appointments.Add(appt);
        }

        public async Task AddAppointment()
        {
            var newAppointment = new Appointment()
            {
                Id = Guid.NewGuid(),
                Title = InputAppointment.Title,
                PatientName = InputAppointment.PatientName,
                Description = InputAppointment.Description,
                ScheduledDate = InputAppointment.ScheduledDate,
            };
            await _apiService.CreateAppointmentAsync(newAppointment);
            Appointments.Add(newAppointment);
            ResetInputForm();
        }

        public async Task UpdateAppointment()
        {
            if (SelectedAppointment == null) return;
            await _apiService.UpdateAppointmentAsync(SelectedAppointment.Id, InputAppointment);
            var appt = Appointments.FirstOrDefault(a => a.Id == SelectedAppointment.Id);
            if (appt != null)
            {
                appt.PatientName = InputAppointment.PatientName;
                appt.Description = InputAppointment.Description;
                appt.Title = InputAppointment.Title;
                appt.ScheduledDate = InputAppointment.ScheduledDate;
            }
            SelectedAppointment = null;
        }

        private void ResetInputForm()
        {
            InputAppointment.Title = string.Empty;
            InputAppointment.Description = string.Empty;
            InputAppointment.PatientName = string.Empty;
            InputAppointment.ScheduledDate = DateTime.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
