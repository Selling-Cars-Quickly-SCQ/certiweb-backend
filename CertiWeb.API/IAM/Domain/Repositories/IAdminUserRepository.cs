using CertiWeb.API.IAM.Domain.Model.Aggregates;
using CertiWeb.API.Shared.Domain.Repositories;

namespace CertiWeb.API.IAM.Domain.Repositories;

/// <summary>
/// Repository interface for AdminUser aggregate
/// </summary>
public interface IAdminUserRepository : IBaseRepository<AdminUser>
{
    /// <summary>
    /// Find admin user by email
    /// </summary>
    /// <param name="email">Admin user email</param>
    /// <returns>Admin user if found, null otherwise</returns>
    Task<AdminUser?> FindByEmailAsync(string email);
    
    /// <summary>
    /// Get all admin users
    /// </summary>
    /// <returns>List of all admin users</returns>
    Task<IEnumerable<AdminUser>> GetAllAsync();
}