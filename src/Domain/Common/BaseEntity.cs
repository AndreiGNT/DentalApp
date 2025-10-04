using System.ComponentModel.DataAnnotations;

namespace DentalApp.Domain.Common;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
}
