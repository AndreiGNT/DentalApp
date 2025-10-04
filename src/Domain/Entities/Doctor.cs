
using System.ComponentModel.DataAnnotations;


namespace DentalApp.Domain.Entities;
public class Doctor : BaseAuditableEntity
{
    [Required]
    public string FullName { get; set; } = string.Empty;

    public ICollection<DoctorProcedure> DoctorProcedures { get; set; } = new List<DoctorProcedure>();
}
