using CertiWeb.API.Certifications.Domain.Model.Aggregates;
using CertiWeb.API.Shared.Domain.Repositories;

namespace CertiWeb.API.Certifications.Domain.Repositories;

/// <summary>
/// Repository interface for brand-related data operations.
/// </summary>
public interface IBrandRepository : IBaseRepository<Brand>
{
    /// <summary>
    /// Finds a brand by its ID.
    /// </summary>
    /// <param name="brandId">The brand ID to search for.</param>
    /// <returns>The brand if found, null otherwise.</returns>
    Task<Brand?> FindBrandByIdAsync(int brandId);
    
    /// <summary>
    /// Gets all active brands.
    /// </summary>
    /// <returns>A collection of active brands.</returns>
    Task<IEnumerable<Brand>> GetActiveBrandsAsync();
}
