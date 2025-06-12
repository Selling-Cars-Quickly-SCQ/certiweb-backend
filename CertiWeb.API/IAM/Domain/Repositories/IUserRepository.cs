using CertiWeb.API.IAM.Domain.Model.Aggregates;
using CertiWeb.API.Shared.Domain.Repositories;

namespace CertiWeb.API.IAM.Domain.Repositories
{
    /// <summary>
    /// Repository interface for User entity
    /// </summary>
    public interface IUserRepository : IBaseRepository<User>
    {
        /// <summary>
        /// Find user by email address
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>User if found, null otherwise</returns>
        Task<User?> FindByEmailAsync(string email);

        /// <summary>
        /// Check if email already exists
        /// </summary>
        /// <param name="email">Email to check</param>
        /// <returns>True if exists, false otherwise</returns>
        Task<bool> ExistsByEmailAsync(string email);
    }
}