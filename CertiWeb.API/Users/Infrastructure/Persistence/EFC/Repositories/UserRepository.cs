using CertiWeb.API.Users.Domain.Model.Aggregates;
using CertiWeb.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CertiWeb.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using CertiWeb.API.Users.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CertiWeb.API.Users.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Entity Framework Core implementation of the user repository.
/// </summary>
public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    /// <summary>
    /// Finds a user by their email address using Entity Framework Core.
    /// </summary>
    /// <param name="email">The email address to search for.</param>
    /// <returns>The user if found, null otherwise.</returns>
    public async Task<User?> FindUserByEmailAsync(string email)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(u => u.email == email);
    }
    
    /// <summary>
    /// Finds a user by their email address and password using Entity Framework Core.
    /// </summary>
    /// <param name="email">The email address to search for.</param>
    /// <param name="password">The password to verify.</param>
    /// <returns>The user if found with matching credentials, null otherwise.</returns>
    public async Task<User?> FindUserByEmailAndPasswordAsync(string email, string password)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(u => u.email == email && u.password == password);
    }
}