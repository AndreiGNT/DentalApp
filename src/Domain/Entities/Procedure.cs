
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DentalApp.Domain.Entities;
public class Procedure : BaseAuditableEntity
{
    [Required]
    [DisplayName("Procedure Name")]
    public string ProcedureName { get; set; } = string.Empty;

    [Required]
    public TimeSpan Duration { get; set; }

    public ICollection<DoctorProcedure> DoctorProcedures { get; set; } = new List<DoctorProcedure>();

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
