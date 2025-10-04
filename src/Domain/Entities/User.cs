
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DentalApp.Domain.Constants;

namespace DentalApp.Domain.Entities;

public class User : BaseAuditableEntity
{
    [Required]
    [MaxLength(50)]
    [DisplayName("First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    [DisplayName("Last Name")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [Phone]
    [DisplayName("Phone Number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = Roles.Client;

    public bool IsConfirmed { get; set; }

    public string? ConfirmationToken { get; set; }
}
