
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentalApp.Domain.Entities;
public class DoctorProcedure : BaseAuditableEntity
{
    [ForeignKey(nameof(Doctor))]
    public int DoctorId { get; set; }
    public Doctor? Doctor { get; set; }

    [ForeignKey(nameof(Procedure))]
    public int ProcedureId { get; set; }
    public Procedure? Procedure { get; set; }
}
