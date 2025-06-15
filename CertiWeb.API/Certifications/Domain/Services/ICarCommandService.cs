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
}