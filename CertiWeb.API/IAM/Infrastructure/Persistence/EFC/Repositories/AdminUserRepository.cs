using CertiWeb.API.IAM.Domain.Model.Aggregates;
using CertiWeb.API.IAM.Domain.Repositories;
using CertiWeb.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CertiWeb.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CertiWeb.API.IAM.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Repository implementation for AdminUser aggregate
/// </summary>
public class AdminUserRepository : BaseRepository<AdminUser>, IAdminUserRepository
{
    /// <summary>
    /// Constructor for AdminUserRepository
    /// </summary>
    /// <param name="context">Database context</param>
    public AdminUserRepository(AppDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Find admin user by email
    /// </summary>
    /// <param name="email">Admin user email</param>
    /// <returns>Admin user if found, null otherwise</returns>
    public async Task<AdminUser?> FindByEmailAsync(string email)
    {
        return await Context.Set<AdminUser>()
            .FirstOrDefaultAsync(u => u.Email == email);
    }
    
    /// <summary>
    /// Get all admin users
    /// </summary>
    /// <returns>List of all admin users</returns>
    public async Task<IEnumerable<AdminUser>> GetAllAsync()
    {
        return await Context.Set<AdminUser>()
            .ToListAsync();
    }
}