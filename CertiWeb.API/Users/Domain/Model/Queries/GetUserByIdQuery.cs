namespace CertiWeb.API.Users.Domain.Model.Queries;

/// <summary>
/// Query for retrieving a user by their unique identifier.
/// </summary>
/// <param name="userId">The ID of the user to retrieve.</param>
public record GetUserByIdQuery(int userId);