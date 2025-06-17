using CertiWeb.API.Certifications.Domain.Model.Aggregates;
using CertiWeb.API.Certifications.Domain.Model.Commands;

namespace CertiWeb.API.Certifications.Domain.Services;

/// <summary>
/// Service interface for handling car command operations.
/// </summary>
public interface ICarCommandService
{
    /// <summary>
    /// Handles the creation of a new car certification.
    /// </summary>
    /// <param name="command">The command containing the car creation data.</param>
    /// <returns>The created car if successful, null if an error occurs.</returns>
    Task<Car?> Handle(CreateCarCommand command);
    
    /// <summary>
    /// Handles the update of an existing car certification.
    /// </summary>
    /// <param name="command">The command containing the car update data.</param>
    /// <returns>The updated car if successful, null if an error occurs.</returns>
    Task<Car?> Handle(UpdateCarCommand command);
    
    /// <summary>
    /// Handles the deletion of a car certification.
    /// </summary>
    /// <param name="command">The command containing the car ID to delete.</param>
    /// <returns>True if the car was deleted successfully, false otherwise.</returns>
    Task<bool> Handle(DeleteCarCommand command);
}