using CertiWeb.API.Users.Domain.Model.Aggregates;
using CertiWeb.API.Users.Domain.Model.Queries;

namespace CertiWeb.API.Users.Domain.Services;

/// <summary>
/// Service interface for handling user query operations.
/// </summary>
public interface IUserQueryService
{
    /// <summary>
    /// Retrieves all users from the system.
    /// </summary>
    /// <param name="query">The query parameters for retrieving all users.</param>
    /// <returns>A collection of all users in the system.</returns>
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query);

    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="query">The query containing the user ID to search for.</param>
    /// <returns>The user if found, null otherwise.</returns>
    Task<User?> Handle(GetUserByIdQuery query);

    /// <summary>
    /// Retrieves a user by their email address.
    /// </summary>
    /// <param name="query">The query containing the email to search for.</param>
    /// <returns>The user if found, null otherwise.</returns>
    Task<User?> Handle(GetUserByEmail query);
}