namespace CertiWeb.API.Users.Interfaces.ACL;

/// <summary>
/// Facade interface for Users context operations.
/// Provides anti-corruption layer for Users bounded context.
/// </summary>
public interface IUsersContextFacade
{
    /// <summary>
    /// Creates a new user profile.
    /// </summary>
    /// <param name="name">The full name of the user</param>
    /// <param name="email">The email of the user</param>
    /// <param name="password">The password of the user</param>
    /// <param name="plan">The subscription plan</param>
    /// <returns>The ID of the created user if successful, 0 otherwise</returns>
    Task<int> CreateUserProfile(string name, string email, string password, string plan);
    
    /// <summary>
    /// Fetches user profile by email.
    /// </summary>
    /// <param name="email">The email to search for</param>
    /// <returns>The user ID if found, 0 otherwise</returns>
    Task<int> FetchUserProfileIdByEmail(string email);
    
    /// <summary>
    /// Fetches user profile information by ID.
    /// </summary>
    /// <param name="userId">The user ID to search for</param>
    /// <returns>A tuple with user information if found, null values otherwise</returns>
    Task<(string name, string email, string plan)?> FetchUserProfileById(int userId);
}