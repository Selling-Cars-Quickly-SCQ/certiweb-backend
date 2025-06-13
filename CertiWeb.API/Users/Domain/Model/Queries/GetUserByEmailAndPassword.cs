namespace CertiWeb.API.Users.Domain.Model.Queries;

/// <summary>
/// Query for retrieving a user by their email address and password for authentication.
/// </summary>
/// <param name="email">The email address to search for.</param>
/// <param name="password">The password to verify.</param>
public record GetUserByEmailAndPassword(string email, string password);