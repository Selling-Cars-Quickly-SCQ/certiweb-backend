using CertiWeb.API.Certifications.Domain.Model.Aggregates;
using CertiWeb.API.Certifications.Domain.Repositories;
using CertiWeb.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CertiWeb.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CertiWeb.API.Certifications.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Entity Framework Core implementation of the brand repository.
/// </summary>
public class BrandRepository(AppDbContext context) : BaseRepository<Brand>(context), IBrandRepository
{
    /// <summary>
    /// Finds a brand by its ID.
    /// </summary>
    /// <param name="brandId">The brand ID to search for.</param>
    /// <returns>The brand if found, null otherwise.</returns>
    public async Task<Brand?> FindBrandByIdAsync(int brandId)
    {
        return await Context.Set<Brand>()
            .FirstOrDefaultAsync(b => b.Id == brandId && b.IsActive);
    }
    
    /// <summary>
    /// Gets all active brands.
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