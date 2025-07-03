using CertiWeb.API.Users.Domain.Model.Aggregates;
using CertiWeb.API.Users.Domain.Model.Queries;
using CertiWeb.API.Users.Domain.Repositories;
using CertiWeb.API.Users.Domain.Services;
using CertiWeb.API.Users.Application.Internal.OutboundServices;

namespace CertiWeb.API.Users.Application.Internal.QueryServices;

/// <summary>
/// Implementation of the user query service that handles user retrieval operations.
/// </summary>
public class UserQueryServiceImpl(IUserRepository userRepository, IHashingService hashingService) : IUserQueryService
{
    /// <summary>
    /// Retrieves all users from the system.
    /// </summary>
    /// <param name="query">The query parameters for retrieving all users.</param>
    /// <returns>A collection of all users in the system.</returns>
    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query)
    {
        return await userRepository.ListAsync();
    }

    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="query">The query containing the user ID to search for.</param>
    /// <returns>The user if found, null otherwise.</returns>
    public async Task<User?> Handle(GetUserByIdQuery query)
    {
        return await userRepository.FindByIdAsync(query.userId);
    }

    /// <summary>
    /// Retrieves a user by their email address.
    /// </summary>
    /// <param name="query">The query containing the email to search for.</param>
    /// <returns>The user if found, null otherwise.</returns>
    public async Task<User?> Handle(GetUserByEmail query)
    {
        return await userRepository.FindUserByEmailAsync(query.email);
    }
    
    /// <summary>
    /// Retrieves a user by their email address and password for authentication.
    /// </summary>
    /// <param name="query">The query containing the email and password to search for.</param>
    /// <returns>The user if found with matching credentials, null otherwise.</returns>
    public async Task<User?> Handle(GetUserByEmailAndPassword query)
    {
        // First, find the user by email
        var user = await userRepository.FindUserByEmailAsync(query.email);
        
        // If user not found, return null
        if (user == null) return null;
        
        // Verify the password using the hashing service
        // query.password = contraseña original del frontend
        // user.password = contraseña hasheada almacenada en BD
        var isPasswordValid = hashingService.VerifyPassword(query.password, user.password);
        
        // Return user only if password is valid
        return isPasswordValid ? user : null;
    }
}