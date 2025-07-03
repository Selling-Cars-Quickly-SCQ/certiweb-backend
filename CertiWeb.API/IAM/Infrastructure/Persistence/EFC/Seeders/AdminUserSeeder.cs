using CertiWeb.API.IAM.Domain.Model.Aggregates;
using BCryptNet = BCrypt.Net.BCrypt;

namespace CertiWeb.API.IAM.Infrastructure.Persistence.EFC.Seeders;

/// <summary>
/// Seeder for predefined admin user data
/// </summary>
public static class AdminUserSeeder
{
    /// <summary>
    /// Gets the predefined admin user for seeding with hashed password
    /// </summary>
    /// <returns>Admin user with predefined credentials and hashed password</returns>
    public static AdminUser GetAdminUser()
    {
        return new AdminUser
        {
            Id = 1,
            Name = "admin",
            Email = "adminEMAIL@gmail.com",
            Password = BCryptNet.HashPassword("admin")
        };
    }
}