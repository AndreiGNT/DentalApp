
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalApp.Domain.Entities;
public class Appointment : BaseAuditableEntity
{
    [ForeignKey(nameof(User))]
    public string? UserId { get; set; }
    
    public ApplicationUser User { get; set; } = new ApplicationUser();

    [ForeignKey(nameof(Doctor))]
    public int DoctorId { get; set; }

    public Doctor Doctor { get; set; } = new Doctor();


    [ForeignKey(nameof(Procedure))]
    public int ProcedureId { get; set; }

    public Procedure Procedure { get; set; } = new Procedure();

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string Status { get; set; } = AppointmentStatus.Pending;
}
