namespace CertiWeb.API.Users.Interfaces.REST.Resources;

/// <summary>
/// Data transfer object for representing a user in the REST API responses.
/// </summary>
/// <param name="Id">The unique identifier of the user.</param>
/// <param name="name">The full name of the user.</param>
/// <param name="email">The email address of the user.</param>
/// <param name="password">The hashed password of the user.</param>
/// <param name="plan">The subscription plan of the user.</param>
public record UserResource(int Id, string name, string email, string password, string plan);