using CertiWeb.API.IAM.Domain.Model.Aggregates;
using CertiWeb.API.IAM.Domain.Repositories;
using CertiWeb.API.IAM.Domain.Services;

namespace CertiWeb.API.IAM.Application.Internal.QueryServices;

/// <summary>
/// Query service implementation for AdminUser operations
/// </summary>
public class AdminUserQueryService : IAdminUserQueryService
{
    private readonly IAdminUserRepository _adminUserRepository;

    /// <summary>
    /// Constructor for AdminUserQueryService
    /// </summary>
    /// <param name="adminUserRepository">Admin user repository</param>
    public AdminUserQueryService(IAdminUserRepository adminUserRepository)
    {
        _adminUserRepository = adminUserRepository;
    }

    /// <summary>
    /// Get all admin users
    /// </summary>
    /// <returns>List of all admin users</returns>
    public async Task<IEnumerable<AdminUser>> GetAllAdminUsersAsync()
    {
        return await _adminUserRepository.GetAllAsync();
    }

    /// <summary>
    /// Get admin user by ID
    /// </summary>
    /// <param name="id">Admin user ID</param>
    /// <returns>Admin user if found, null otherwise</returns>
    public async Task<AdminUser?> GetAdminUserByIdAsync(int id)
    {
        return await _adminUserRepository.FindByIdAsync(id);
    }

    /// <summary>
    /// Get admin user by email
    /// </summary>
    /// <param name="email">Admin user email</param>
    /// <returns>Admin user if found, null otherwise</returns>
    public async Task<AdminUser?> GetAdminUserByEmailAsync(string email)
    {
        return await _adminUserRepository.FindByEmailAsync(email);
    }
}