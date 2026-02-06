namespace AppointmentManagementSystem.Api.Models;

public class Appointment
{
    public Appointment(Guid id, string patientName)
    {
        this.Id = id;
        this.PatientName = patientName;
    }

    public Guid Id { get; set; }
    public string PatientName { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime ScheduledDate { get; set; }
}
