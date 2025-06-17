using CertiWeb.API.Shared.Domain.Repositories;
using CertiWeb.API.Users.Domain.Model.Aggregates;

namespace CertiWeb.API.Users.Domain.Repositories;

/// <summary>
/// Repository interface for user-related data operations.
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    /// Finds a user by their email address.
    /// </summary>
    /// <param name="email">The email address to search for.</param>
    /// <returns>The user if found, null otherwise.</returns>
    Task<User?> FindUserByEmailAsync(string email);
    
    /// <summary>
    /// Finds a user by their email address and password for authentication.
    /// </summary>
    /// <param name="email">The email address to search for.</param>
    /// <param name="password">The password to verify.</param>
    /// <returns>The user if found with matching credentials, null otherwise.</returns>
    Task<User?> FindUserByEmailAndPasswordAsync(string email, string password);
}