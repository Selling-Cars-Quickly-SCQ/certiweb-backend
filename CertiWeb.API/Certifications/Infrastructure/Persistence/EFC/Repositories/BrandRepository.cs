using CertiWeb.API.Certifications.Domain.Model.Aggregates;
using CertiWeb.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CertiWeb.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using CertiWeb.API.Certifications.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CertiWeb.API.Certifications.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Entity Framework Core implementation of the brand repository.
/// </summary>
public class BrandRepository(AppDbContext context) : BaseRepository<Brand>(context), IBrandRepository
{
    /// <summary>
    /// Finds a brand by name using Entity Framework Core.
    /// </summary>
    /// <param name="name">The brand name to search for.</param>
    /// <returns>The brand if found, null otherwise.</returns>
    public async Task<Brand?> FindBrandByNameAsync(string name)
    {
        return await Context.Set<Brand>().FirstOrDefaultAsync(b => b.Name == name);
    }

    /// <summary>
    /// Gets all active brands using Entity Framework Core.
    /// </summary>
    /// <returns>A collection of active brands.</returns>
    public async Task<IEnumerable<Brand>> GetActiveBrandsAsync()
    {
        return await Context.Set<Brand>()
            .Where(b => b.IsActive)
            .OrderBy(b => b.Name)
            .ToListAsync();
    }
}