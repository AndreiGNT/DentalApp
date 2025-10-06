
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DentalApp.Domain.Entities;
public class Procedure : BaseAuditableEntity
{
    [Required]
    [DisplayName("Procedure Name")]
    [MaxLength(200)]
    public string ProcedureName { get; set; } = string.Empty;

    [Required]
    public TimeSpan Duration { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Price must be a positive number.")]
    public int Price { get; set; }
}

