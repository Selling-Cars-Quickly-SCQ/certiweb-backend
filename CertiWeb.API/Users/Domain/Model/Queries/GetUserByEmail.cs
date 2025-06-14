namespace CertiWeb.API.Users.Domain.Model.Queries;

/// <summary>
/// Query for retrieving a user by their email address.
/// </summary>
/// <param name="email">The email address to search for.</param>
public record GetUserByEmail(string email);