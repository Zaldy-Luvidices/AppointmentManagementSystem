using AppointmentManagementSystem.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManagementSystem.Api.Data;

public class AppDbContext : DbContext
{
    public DbSet<Appointment> Appointments { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>().HasKey(a => a.Id);
        base.OnModelCreating(modelBuilder);
    }
}
