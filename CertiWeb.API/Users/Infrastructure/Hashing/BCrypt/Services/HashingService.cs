using CertiWeb.API.Users.Application.Internal.OutboundServices;
using BCryptNet = BCrypt.Net.BCrypt;

namespace CertiWeb.API.Users.Infrastructure.Hashing.BCrypt.Services;

/// <summary>
/// Implementation of password hashing service using BCrypt algorithm.
/// Provides functionality to hash passwords and verify hashed passwords.
/// </summary>
public class HashingService : IHashingService
{
    /// <summary>
    /// Hashes a plain text password using the BCrypt algorithm.
    /// </summary>
    /// <param name="password">The plain text password to be hashed.</param>
    /// <returns>The hashed password as a string.</returns>
    /// <exception cref="ArgumentException">Thrown when the password is null, empty, or contains only whitespace.</exception>
    public string HashPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be null or empty", nameof(password));
            
        return BCryptNet.HashPassword(password);
    }

    /// <summary>
    /// Verifies if a plain text password matches a stored password hash.
    /// </summary>
    /// <param name="password">The plain text password to verify.</param>
    /// <param name="passwordHash">The stored password hash to verify against.</param>
    /// <returns>true if the password matches the hash; otherwise, false.</returns>
    /// <exception cref="ArgumentException">Thrown when the password or password hash are null, empty, or contain only whitespace.</exception>
    public bool VerifyPassword(string password, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be null or empty", nameof(password));
            
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash cannot be null or empty", nameof(passwordHash));
            
        return BCryptNet.Verify(password, passwordHash);
    }
}