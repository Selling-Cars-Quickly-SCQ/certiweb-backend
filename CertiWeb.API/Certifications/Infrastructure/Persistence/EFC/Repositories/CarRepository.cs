using CertiWeb.API.Certifications.Domain.Model.Aggregates;
using CertiWeb.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CertiWeb.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using CertiWeb.API.Certifications.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CertiWeb.API.Certifications.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Entity Framework Core implementation of the car repository.
/// </summary>
public class CarRepository(AppDbContext context) : BaseRepository<Car>(context), ICarRepository
{
    /// <summary>
    /// Finds cars by brand ID using Entity Framework Core.
    /// </summary>
    /// <param name="brandId">The brand ID to search for.</param>
    /// <returns>A collection of cars for the specified brand.</returns>
    public async Task<IEnumerable<Car>> FindCarsByBrandIdAsync(int brandId)
    {
        return await Context.Set<Car>()
            .Include(c => c.Brand)
            .Where(c => c.BrandId == brandId)
            .ToListAsync();
    }

    /// <summary>
    /// Finds cars by owner email using Entity Framework Core.
    /// </summary>
    /// <param name="ownerEmail">The owner email to search for.</param>
    /// <returns>A collection of cars for the specified owner.</returns>
    public async Task<IEnumerable<Car>> FindCarsByOwnerEmailAsync(string ownerEmail)
    {
        return await Context.Set<Car>()
            .Include(c => c.Brand)
            .Where(c => c.OwnerEmail == ownerEmail)
            .ToListAsync();
    }

    /// <summary>
    /// Finds a car by license plate using Entity Framework Core.
    /// </summary>
    /// <param name="licensePlate">The license plate to search for.</param>
    /// <returns>The car if found, null otherwise.</returns>
    public async Task<Car?> FindCarByLicensePlateAsync(string licensePlate)
    {
        return await Context.Set<Car>()
            .Include(c => c.Brand)
            .FirstOrDefaultAsync(c => c.LicensePlate == licensePlate);
    }

    /// <summary>
    /// Finds a car by original reservation ID using Entity Framework Core.
    /// </summary>
    /// <param name="reservationId">The reservation ID to search for.</param>
    /// <returns>The car if found, null otherwise.</returns>
    public async Task<Car?> FindCarByReservationIdAsync(int reservationId)
    {
        return await Context.Set<Car>()
            .Include(c => c.Brand)
            .FirstOrDefaultAsync(c => c.OriginalReservationId == reservationId);
    }
}