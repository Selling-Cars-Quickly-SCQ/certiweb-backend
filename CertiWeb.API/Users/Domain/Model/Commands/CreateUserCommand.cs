
namespace CertiWeb.API.Users.Domain.Model.Commands;

/// <summary>
/// Command for creating a new user in the system.
/// </summary>
/// <param name="name">The full name of the user.</param>
/// <param name="email">The email address of the user.</param>
/// <param name="password">The password for the user account.</param>
/// <param name="plan">The subscription plan for the user.</param>
public record CreateUserCommand(string name, string email, string password, string plan);