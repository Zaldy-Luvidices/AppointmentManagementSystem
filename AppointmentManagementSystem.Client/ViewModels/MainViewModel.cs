using AppointmentManagementSystem.Client.Commands;
using AppointmentManagementSystem.Client.Exceptions;
using AppointmentManagementSystem.Client.Models;
using AppointmentManagementSystem.Client.Services;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<MainViewModel> _logger;

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

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainViewModel(
            IApiService apiService,
            ILogger<MainViewModel> logger)
        {
            _apiService = apiService;
            _logger = logger;

            AddCommand = new RelayCommand(async () => await AddAppointment());
            UpdateCommand = new RelayCommand(async () => await UpdateAppointment());
            DeleteCommand = new RelayCommand(async () => await DeleteAppointment());
            _ = LoadAppointments();
        }

        public async Task LoadAppointments()
        {
            try
            {
                _logger.LogInformation("Loading appointments...");
                var appointments = await _apiService.GetAppointmentsAsync();
                Appointments.Clear();
                foreach (var appt in appointments) Appointments.Add(appt);
                _logger.LogInformation("Appointments loaded: {Count}", Appointments.Count);
                ErrorMessage = string.Empty;
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, $"Failed: {ex.Message}");
                ErrorMessage = $"Failed to load appointments: {ex.Message}";
            }
            catch (Exception)
            {
                _logger.LogError($"Failed: Unexpected error");
                ErrorMessage = "Unexpected error occurred.";
            }
        }

        public async Task AddAppointment()
        {
            try
            {
                _logger.LogInformation($"Adding appointment for {InputAppointment.PatientName}...");
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
                _logger.LogInformation("Success");
                ErrorMessage = string.Empty;
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, $"Failed: {ex.Message}");
                ErrorMessage = $"Failed to add appointment: {ex.Message}";
            }
            catch (Exception)
            {
                _logger.LogError($"Failed: Unexpected error");
                ErrorMessage = "Unexpected error occurred.";
            }
        }

        public async Task UpdateAppointment()
        {
            try
            {
                if (SelectedAppointment == null) return;
                _logger.LogInformation($"Updating appointment {SelectedAppointment.Id}...");
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
                _logger.LogInformation("Success");
                ErrorMessage = string.Empty;
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, $"Failed: {ex.Message}");
                ErrorMessage = $"Failed to update appointment: {ex.Message}";
            }
            catch (Exception)
            {
                _logger.LogError($"Failed: Unexpected error");
                ErrorMessage = "Unexpected error occurred.";
            }
        }

        public async Task DeleteAppointment()
        {
            try
            {
                if (SelectedAppointment == null) return;
                _logger.LogInformation($"Deleting appointment {SelectedAppointment.Id}...");
                await _apiService.DeleteAppointmentAsync(SelectedAppointment.Id);
                Appointments.Remove(SelectedAppointment);
                SelectedAppointment = null;
                _logger.LogInformation("Success");
                ErrorMessage = string.Empty;
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, $"Failed: {ex.Message}");
                ErrorMessage = $"Failed to delete appointment: {ex.Message}";
            }
            catch (Exception)
            {
                _logger.LogError($"Failed: Unexpected error");
                ErrorMessage = "Unexpected error occurred.";
            }
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
