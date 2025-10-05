using Microsoft.AspNetCore.Identity;

namespace DentalApp.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool IsConfirmed { get; set; }
    public string? ConfirmationToken { get; set; }
}
