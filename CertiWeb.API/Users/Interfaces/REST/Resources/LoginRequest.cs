namespace CertiWeb.API.Users.Interfaces.REST.Resources;

/// <summary>
/// Request resource for user login containing email and password credentials.
/// </summary>
/// <param name="Email">The user's email address.</param>
/// <param name="Password">The user's password.</param>
public record LoginRequest(string Email, string Password);