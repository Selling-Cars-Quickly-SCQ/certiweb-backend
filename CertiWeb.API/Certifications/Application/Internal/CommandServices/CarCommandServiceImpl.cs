using CertiWeb.API.Certifications.Domain.Model.Aggregates;
using CertiWeb.API.Certifications.Domain.Model.Commands;
using CertiWeb.API.Certifications.Domain.Services;
using CertiWeb.API.Shared.Domain.Repositories;
using CertiWeb.API.Certifications.Domain.Repositories;

namespace CertiWeb.API.Certifications.Application.Internal.CommandServices;

/// <summary>
/// Implementation of the car command service that handles car creation operations.
/// </summary>
public class CarCommandServiceImpl(ICarRepository carRepository, IUnitOfWork unitOfWork) : ICarCommandService
{
    /// <summary>
    /// Handles the creation of a new car certification in the system.
    /// </summary>
    /// <param name="command">The command containing the car creation data.</param>
    /// <returns>The created car if successful, null if an error occurs.</returns>
    public async Task<Car?> Handle(CreateCarCommand command)
    {
        // Validate that the reservation hasn't been used already
        var existingCar = await carRepository.FindCarByReservationIdAsync(command.OriginalReservationId);
        if (existingCar != null)
        {
            return null; // Reservation already used
        }
        
        // Validate license plate uniqueness
        var existingLicensePlate = await carRepository.FindCarByLicensePlateAsync(command.LicensePlate);
        if (existingLicensePlate != null)
        {
            return null;
        }
        
        var car = new Car(command);
        try
        {
            await carRepository.AddAsync(car);
            await unitOfWork.CompleteAsync();
            return car;
        }
        catch (Exception)
        {
            return null;
        }
    }
}