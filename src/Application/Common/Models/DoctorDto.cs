using DentalApp.Domain.Entities;

namespace DentalApp.Application.Common.Models;
public class DoctorDto
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public List<string> Procedures { get; set; } = new();

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
