namespace CertiWeb.API.Users.Interfaces.REST.Resources;

/// <summary>
/// Data transfer object for representing an authenticated user with JWT token.
/// </summary>
/// <param name="Id">The unique identifier of the user.</param>
/// <param name="Name">The full name of the user.</param>
/// <param name="Email">The email address of the user.</param>
/// <param name="Plan">The subscription plan of the user.</param>
/// <param name="Token">The JWT authentication token.</param>
public record AuthenticatedUserResource(int Id, string Name, string Email, string Plan, string Token);