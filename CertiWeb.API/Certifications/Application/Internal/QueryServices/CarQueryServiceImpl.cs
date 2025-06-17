using CertiWeb.API.Certifications.Domain.Model.Aggregates;
using CertiWeb.API.Certifications.Domain.Model.Queries;
using CertiWeb.API.Certifications.Domain.Repositories;
using CertiWeb.API.Certifications.Domain.Services;

namespace CertiWeb.API.Certifications.Application.Internal.QueryServices;

/// <summary>
/// Implementation of the car query service that handles car retrieval operations.
/// </summary>
public class CarQueryServiceImpl(ICarRepository carRepository) : ICarQueryService
{
    /// <summary>
    /// Retrieves all cars from the system.
    /// </summary>
    /// <param name="query">The query parameters for retrieving all cars.</param>
    /// <returns>A collection of all cars in the system.</returns>
    public async Task<IEnumerable<Car>> Handle(GetAllCarsQuery query)
    {
        return await carRepository.ListAsync();
    }

    /// <summary>
    /// Retrieves a car by its unique identifier.
    /// </summary>
    /// <param name="query">The query containing the car ID to search for.</param>
    /// <returns>The car if found, null otherwise.</returns>
    public async Task<Car?> Handle(GetCarByIdQuery query)
    {
        return await carRepository.FindByIdAsync(query.Id);
    }

    /// <summary>
    /// Retrieves cars by brand ID.
    /// </summary>
    /// <param name="query">The query containing the brand ID to search for.</param>
    /// <returns>A collection of cars for the specified brand.</returns>
    public async Task<IEnumerable<Car>> Handle(GetCarsByBrandQuery query)
    {
        return await carRepository.FindCarsByBrandIdAsync(query.BrandId);
    }
    
    /// <summary>
    /// Retrieves cars by owner email.
    /// </summary>
    /// <param name="query">The query containing the owner email to search for.</param>
    /// <returns>A collection of cars for the specified owner.</returns>
    public async Task<IEnumerable<Car>> Handle(GetCarsByOwnerEmailQuery query)
    {
        return await carRepository.FindCarsByOwnerEmailAsync(query.OwnerEmail);
    }
}