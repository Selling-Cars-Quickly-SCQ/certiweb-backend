using CertiWeb.API.Certifications.Domain.Model.Aggregates;
using CertiWeb.API.Shared.Domain.Repositories;

namespace CertiWeb.API.Certifications.Domain.Repositories;

/// <summary>
/// Repository interface for car-related data operations.
/// </summary>
public interface ICarRepository : IBaseRepository<Car>
{
    /// <summary>
    /// Finds cars by brand ID.
    /// </summary>
    /// <param name="brandId">The brand ID to search for.</param>
    /// <returns>A collection of cars for the specified brand.</returns>
    Task<IEnumerable<Car>> FindCarsByBrandIdAsync(int brandId);
    
    /// <summary>
    /// Finds cars by owner email.
    /// </summary>
    /// <param name="ownerEmail">The owner email to search for.</param>
    /// <returns>A collection of cars for the specified owner.</returns>
    Task<IEnumerable<Car>> FindCarsByOwnerEmailAsync(string ownerEmail);
    
    /// <summary>
    /// Finds a car by license plate.
    /// </summary>
    /// <param name="licensePlate">The license plate to search for.</param>
    /// <returns>The car if found, null otherwise.</returns>
    Task<Car?> FindCarByLicensePlateAsync(string licensePlate);
    
    /// <summary>
    /// Finds cars by original reservation ID.
    /// </summary>
    /// <param name="reservationId">The reservation ID to search for.</param>
    /// <returns>The car if found, null otherwise.</returns>
    Task<Car?> FindCarByReservationIdAsync(int reservationId);
}