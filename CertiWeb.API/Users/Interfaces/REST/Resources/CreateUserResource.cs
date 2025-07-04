using System.ComponentModel.DataAnnotations;

namespace CertiWeb.API.Users.Interfaces.REST.Resources;

/// <summary>
/// Data transfer object for creating a new user through the REST API.
/// </summary>
/// <param name="name">The full name of the user.</param>
/// <param name="email">The email address of the user.</param>
/// <param name="password">The password for the user account.</param>
/// <param name="plan">The subscription plan for the user.</param>
public record CreateUserResource(
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
    string name,
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    string email,
    
    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters")]
    string password,
    
    [Required(ErrorMessage = "Plan is required")]
    string plan
);