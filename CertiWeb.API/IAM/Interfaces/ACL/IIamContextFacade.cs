namespace CertiWeb.API.IAM.Interfaces.ACL;

/// <summary>
/// Facade interface for IAM context operations.
/// Provides anti-corruption layer for IAM bounded context.
/// </summary>
public interface IIamContextFacade
{
    /// <summary>
    /// Creates a new user in the IAM context.
    /// </summary>
    /// <param name="email">The email of the user</param>
    /// <param name="password">The password of the user</param>
    /// <returns>The ID of the created user if successful, 0 otherwise</returns>
    Task<int> CreateUser(string email, string password);
    
    /// <summary>
    /// Fetches user ID by email.
    /// </summary>
    /// <param name="email">The email to search for</param>
    /// <returns>The user ID if found, 0 otherwise</returns>
    Task<int> FetchUserIdByEmail(string email);
    
    /// <summary>
    /// Fetches user email by user ID.
    /// </summary>
    /// <param name="userId">The user ID to search for</param>
    /// <returns>The email if found, empty string otherwise</returns>
    Task<string> FetchEmailByUserId(int userId);
    
    /// <summary>
    /// Validates user credentials.
    /// </summary>
    /// <param name="email">The email of the user</param>
    /// <param name="password">The password of the user</param>
    /// <returns>The user ID if credentials are valid, 0 otherwise</returns>
    Task<int> ValidateUserCredentials(string email, string password);
}