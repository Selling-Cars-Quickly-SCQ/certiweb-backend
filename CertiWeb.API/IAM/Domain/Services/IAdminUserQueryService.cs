using CertiWeb.API.IAM.Domain.Model.Aggregates;

namespace CertiWeb.API.IAM.Domain.Services;

/// <summary>
/// Query service interface for AdminUser operations
/// </summary>
public interface IAdminUserQueryService
{
    /// <summary>
    /// Get all admin users
    /// </summary>
    /// <returns>List of all admin users</returns>
    Task<IEnumerable<AdminUser>> GetAllAdminUsersAsync();
    
    /// <summary>
    /// Get admin user by ID
    /// </summary>
    /// <param name="id">Admin user ID</param>
    /// <returns>Admin user if found, null otherwise</returns>
    Task<AdminUser?> GetAdminUserByIdAsync(int id);
    
    /// <summary>
    /// Get admin user by email
    /// </summary>
    /// <param name="email">Admin user email</param>
    /// <returns>Admin user if found, null otherwise</returns>
    Task<AdminUser?> GetAdminUserByEmailAsync(string email);
}