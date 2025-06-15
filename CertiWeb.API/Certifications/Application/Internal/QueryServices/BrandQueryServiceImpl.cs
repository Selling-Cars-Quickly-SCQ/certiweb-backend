using CertiWeb.API.Certifications.Domain.Model.Aggregates;
using CertiWeb.API.Certifications.Domain.Repositories;

namespace CertiWeb.API.Certifications.Application.Internal.QueryServices;

/// <summary>
/// Implementation of the brand query service that handles brand retrieval operations.
/// </summary>
public class BrandQueryServiceImpl(IBrandRepository brandRepository)
{
    /// <summary>
    /// Retrieves all active brands from the system.
    /// </summary>
    /// <returns>A collection of all active brands in the system.</returns>
    public async Task<IEnumerable<Brand>> GetAllActiveBrandsAsync()
    {
        return await brandRepository.GetActiveBrandsAsync();
    }

    /// <summary>
    /// Retrieves a brand by its unique identifier.
    /// </summary>
    /// <param name="id">The brand ID to search for.</param>
    /// <returns>The brand if found, null otherwise.</returns>
    public async Task<Brand?> GetBrandByIdAsync(int id)
    {
        return await brandRepository.FindByIdAsync(id);
    }
}