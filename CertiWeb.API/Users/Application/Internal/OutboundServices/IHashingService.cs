namespace CertiWeb.API.Users.Application.Internal.OutboundServices;

/**
 * <summary>
 *     The hashing service interface
 * </summary>
 * <remarks>
 *     This interface is used to hash and verify passwords
 * </remarks>
 */
public interface IHashingService
{
    /**
     * <summary>
     *     Hash a password
     * </summary>
     * <param name="password">The password to hash</param>
     * <returns>The hashed password</returns>
     */
    string HashPassword(string password);

    /**
     * <summary>
     *     Verify a password
     * </summary>
     * <param name="password">The password to verify</param>
     * <param name="passwordHash">The password hash to verify against</param>
     * <returns>True if the password is valid, false otherwise</returns>
     */
    bool VerifyPassword(string password, string passwordHash);
}