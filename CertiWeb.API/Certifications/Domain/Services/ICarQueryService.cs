using CertiWeb.API.Certifications.Domain.Model.Aggregates;
using CertiWeb.API.Certifications.Domain.Model.Queries;

namespace CertiWeb.API.Certifications.Domain.Services;

/// <summary>
/// Service interface for handling car query operations.
/// </summary>
public interface ICarQueryService
{
    /// <summary>
    /// Handles the retrieval of all cars.
    /// </summary>
    /// <param name="query">The query for getting all cars.</param>
    /// <returns>A collection of all cars.</returns>
    Task<IEnumerable<Car>> Handle(GetAllCarsQuery query);
    
    /// <summary>
    /// Handles the retrieval of a car by ID.
    /// </summary>
    /// <param name="query">The query containing the car ID.</param>
    /// <returns>The car if found, null otherwise.</returns>
    Task<Car?> Handle(GetCarByIdQuery query);
    
    /// <summary>
    /// Handles the retrieval of cars by brand.
    /// </summary>
    /// <param name="query">The query containing the brand ID.</param>
    /// <returns>A collection of cars for the specified brand.</returns>
    Task<IEnumerable<Car>> Handle(GetCarsByBrandQuery query);
    
    /// <summary>
    /// Handles the retrieval of cars by owner email.
    /// </summary>
    /// <param name="query">The query containing the owner email.</param>
    /// <returns>A collection of cars for the specified owner.</returns>
    Task<IEnumerable<Car>> Handle(GetCarsByOwnerEmailQuery query);
}