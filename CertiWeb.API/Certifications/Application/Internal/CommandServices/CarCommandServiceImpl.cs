using CertiWeb.API.Certifications.Domain.Model.Aggregates;
using CertiWeb.API.Certifications.Domain.Model.Commands;
using CertiWeb.API.Certifications.Domain.Model.ValueObjects;
using CertiWeb.API.Certifications.Domain.Services;
using CertiWeb.API.Shared.Domain.Repositories;
using CertiWeb.API.Certifications.Domain.Repositories;

namespace CertiWeb.API.Certifications.Application.Internal.CommandServices;

/// <summary>
/// Implementation of the car command service that handles car creation and update operations.
/// </summary>
public class CarCommandServiceImpl(ICarRepository carRepository, IBrandRepository brandRepository, IUnitOfWork unitOfWork) : ICarCommandService
{
    /// <summary>
    /// Handles the creation of a new car certification in the system.
    /// </summary>
    /// <param name="command">The command containing the car creation data.</param>
    /// <returns>The created car if successful, null if an error occurs.</returns>
    public async Task<Car?> Handle(CreateCarCommand command)
    {
        try
        {
            Console.WriteLine($"Creating car with data: Title={command.Title}, Owner={command.Owner}, Year={command.Year}, BrandId={command.BrandId}, Model={command.Model}, Price={command.Price}, LicensePlate={command.LicensePlate}, OriginalReservationId={command.OriginalReservationId}");
            Console.WriteLine($"PdfCertification length: {command.PdfCertification?.Length ?? 0}");
            
            var brand = await brandRepository.FindBrandByIdAsync(command.BrandId);
            if (brand == null)
            {
                Console.WriteLine($"Brand with ID {command.BrandId} not found");
                return null;
            }

            var existingCar = await carRepository.FindCarByReservationIdAsync(command.OriginalReservationId);
            if (existingCar != null)
            {
                Console.WriteLine($"Reservation {command.OriginalReservationId} already used");
                return null;
            }

            var existingLicensePlate = await carRepository.FindCarByLicensePlateAsync(command.LicensePlate);
            if (existingLicensePlate != null)
            {
                Console.WriteLine($"License plate {command.LicensePlate} already exists");
                return null;
            }

            var car = new Car(command);

            car.Brand = brand;

            await carRepository.AddAsync(car);
            await unitOfWork.CompleteAsync();

            Console.WriteLine($"Car created successfully with ID: {car.Id}");
            return car;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Validation error creating car: {ex.Message}");
            Console.WriteLine($"Parameter: {ex.ParamName}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating car: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            return null;
        }
    }
    
    /// <summary>
    /// Handles the update of an existing car certification in the system.
    /// </summary>
    /// <param name="command">The command containing the car update data.</param>
    /// <returns>The updated car if successful, null if an error occurs.</returns>
    public async Task<Car?> Handle(UpdateCarCommand command)
    {
        try
        {
            Console.WriteLine($"Updating car with ID: {command.Id}");
            
            var existingCar = await carRepository.FindByIdAsync(command.Id);
            if (existingCar == null)
            {
                Console.WriteLine($"Car with ID {command.Id} not found");
                return null;
            }

            if (command.BrandId.HasValue)
            {
                var brand = await brandRepository.FindBrandByIdAsync(command.BrandId.Value);
                if (brand == null)
                {
                    Console.WriteLine($"Brand with ID {command.BrandId} not found");
                    return null;
                }
                existingCar.Brand = brand;
            }

            if (!string.IsNullOrEmpty(command.LicensePlate) && command.LicensePlate != existingCar.LicensePlate.Value)
            {
                var existingLicensePlate = await carRepository.FindCarByLicensePlateAsync(command.LicensePlate);
                if (existingLicensePlate != null)
                {
                    Console.WriteLine($"License plate {command.LicensePlate} already exists");
                    return null;
                }
            }

            if (!string.IsNullOrEmpty(command.Title))
                existingCar.Title = command.Title;
            
            if (!string.IsNullOrEmpty(command.Owner))
                existingCar.Owner = command.Owner;
            
            if (!string.IsNullOrEmpty(command.OwnerEmail))
                existingCar.OwnerEmail = command.OwnerEmail;
            
            if (command.Year.HasValue)
                existingCar.Year = new Year(command.Year.Value);
            
            if (!string.IsNullOrEmpty(command.Model))
                existingCar.Model = command.Model;
            
            if (command.Description != null)
                existingCar.Description = command.Description;
            
            if (!string.IsNullOrEmpty(command.PdfCertification))
                existingCar.PdfCertification = new PdfCertification(command.PdfCertification);
            
            if (command.ImageUrl != null)
                existingCar.ImageUrl = command.ImageUrl;
            
            if (command.Price.HasValue)
                existingCar.Price = new Price(command.Price.Value);
            
            if (!string.IsNullOrEmpty(command.LicensePlate))
                existingCar.LicensePlate = new LicensePlate(command.LicensePlate);

            carRepository.Update(existingCar);
            await unitOfWork.CompleteAsync();

            Console.WriteLine($"Car updated successfully with ID: {existingCar.Id}");
            return existingCar;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Validation error updating car: {ex.Message}");
            Console.WriteLine($"Parameter: {ex.ParamName}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating car: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            return null;
        }
    }

    /// <summary>
    /// Handles the deletion of a car certification.
    /// </summary>
    /// <param name="command">The command containing the car ID to delete.</param>
    /// <returns>True if the car was deleted successfully, false otherwise.</returns>
    public async Task<bool> Handle(DeleteCarCommand command)
    {
        try
        {
            var car = await carRepository.FindByIdAsync(command.Id);
            if (car == null)
            {
                Console.WriteLine($"Car with ID {command.Id} not found for deletion");
                return false;
            }

            carRepository.Remove(car);
            await unitOfWork.CompleteAsync();
            
            Console.WriteLine($"Car with ID {command.Id} deleted successfully");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting car with ID {command.Id}: {ex.Message}");
            return false;
        }
    }
}