namespace CertiWeb.API.IAM.Interfaces.REST.Resources;

/// <summary>
/// Request model for admin user login containing email and password credentials.
/// </summary>
/// <param name="Email">The admin user's email address</param>
/// <param name="Password">The admin user's password in plain text</param>
public record LoginAdminRequest(string Email, string Password);