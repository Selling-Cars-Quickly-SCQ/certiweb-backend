namespace CertiWeb.API.Users.Interfaces.REST.Resources;

/// <summary>
/// Data transfer object for user authentication requests.
/// Contains the email and password credentials required for user login.
/// </summary>
/// <param name="Email">The email address of the user attempting to log in.</param>
/// <param name="Password">The plain-text password provided by the user for authentication.</param>
public record LoginRequest(string Email, string Password);