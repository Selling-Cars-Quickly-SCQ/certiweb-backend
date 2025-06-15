using CertiWeb.API.IAM.Domain.Model.Aggregates;

namespace CertiWeb.API.IAM.Infrastructure.Persistence.EFC.Seeders;

/// <summary>
/// Seeder for predefined admin user data
/// </summary>
public static class AdminUserSeeder
{
    /// <summary>
    /// Gets the predefined admin user for seeding
    /// </summary>
    /// <returns>Admin user with predefined credentials</returns>
    public static AdminUser GetAdminUser()
    {
        return new AdminUser
        {
            Id = 1,
            Name = "admin",
            Email = "adminEMAIL@gmail.com",
            Password = "admin"
        };
    }
}