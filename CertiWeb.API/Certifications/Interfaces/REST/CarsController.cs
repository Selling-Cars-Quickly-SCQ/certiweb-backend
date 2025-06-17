using System.Net.Mime;
using CertiWeb.API.Certifications.Domain.Model.Aggregates;
using CertiWeb.API.Certifications.Domain.Model.Commands;
using CertiWeb.API.Certifications.Domain.Model.Queries;
using CertiWeb.API.Certifications.Domain.Services;
using CertiWeb.API.Certifications.Interfaces.REST.Resources;
using CertiWeb.API.Certifications.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CertiWeb.API.Certifications.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Car Certification Endpoints.")]
/// <summary>
/// REST API controller for managing car certification operations.
/// </summary>
public class CarsController(ICarCommandService carCommandService, ICarQueryService carQueryService) : ControllerBase
{
    /// <summary>
    /// Creates a new car certification in the system.
    /// </summary>
    /// <param name="resource">The car creation data.</param>
    /// <returns>The created car resource if successful, BadRequest if creation fails.</returns>
    [HttpPost]
    public async Task<ActionResult<CarResource>> CreateCar([FromBody] CreateCarResource resource)
    {
        try
        {
            Console.WriteLine($"Received CreateCarResource: {System.Text.Json.JsonSerializer.Serialize(resource)}");
            
            var createCarCommand = CreateCarCommandFromResourceAssembler.ToCommandFromResource(resource);
            var car = await carCommandService.Handle(createCarCommand);
            
            if (car == null) 
            {
                return BadRequest(new { 
                    message = "Failed to create car certification.", 
                    details = "Check if reservation ID is already used, license plate already exists, brand ID is valid, or validation requirements are met.",
                    validationRules = new {
                        year = "Must be between 1900 and current year + 1",
                        licensePlate = "Must be between 6 and 10 characters",
                        price = "Must be non-negative",
                        pdfCertification = "Must be a valid Base64 string with at least 10 characters"
                    }
                });
            }
            
            var carResource = CarResourceFromEntityAssembler.ToResourceFromEntity(car);
            return CreatedAtAction(nameof(GetCarById), new { carId = car.Id }, carResource);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Validation error in CreateCar: {ex.Message}");
            return BadRequest(new { 
                message = "Validation error", 
                details = ex.Message,
                parameter = ex.ParamName
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error in CreateCar: {ex.Message}");
            return StatusCode(500, new { message = "Internal server error", details = ex.Message });
        }
    }

    /// <summary>
    /// Retrieves all cars from the system.
    /// </summary>
    /// <returns>A collection of all car resources.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CarResource>>> GetAllCars()
    {
        var getAllCarsQuery = new GetAllCarsQuery();
        var cars = await carQueryService.Handle(getAllCarsQuery);
        var carResources = cars.Select(CarResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(carResources);
    }

    /// <summary>
    /// Retrieves a specific car by its ID.
    /// </summary>
    /// <param name="carId">The ID of the car to retrieve.</param>
    /// <returns>The car resource if found, NotFound if the car doesn't exist.</returns>
    [HttpGet("{carId:int}")]
    public async Task<ActionResult<CarResource>> GetCarById(int carId)
    {
        var getCarByIdQuery = new GetCarByIdQuery(carId);
        var car = await carQueryService.Handle(getCarByIdQuery);
        if (car == null) return NotFound();
        var carResource = CarResourceFromEntityAssembler.ToResourceFromEntity(car);
        return Ok(carResource);
    }

    /// <summary>
    /// Retrieves cars by brand ID.
    /// </summary>
    /// <param name="brandId">The ID of the brand to filter by.</param>
    /// <returns>A collection of car resources for the specified brand.</returns>
    [HttpGet("brand/{brandId:int}")]
    public async Task<ActionResult<IEnumerable<CarResource>>> GetCarsByBrand(int brandId)
    {
        var getCarsByBrandQuery = new GetCarsByBrandQuery(brandId);
        var cars = await carQueryService.Handle(getCarsByBrandQuery);
        var carResources = cars.Select(CarResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(carResources);
    }

    /// <summary>
    /// Retrieves cars by owner email.
    /// </summary>
    /// <param name="ownerEmail">The email of the owner to filter by.</param>
    /// <returns>A collection of car resources for the specified owner.</returns>
    [HttpGet("owner/{ownerEmail}")]
    public async Task<ActionResult<IEnumerable<CarResource>>> GetCarsByOwner(string ownerEmail)
    {
        var getCarsByOwnerQuery = new GetCarsByOwnerEmailQuery(ownerEmail);
        var cars = await carQueryService.Handle(getCarsByOwnerQuery);
        var carResources = cars.Select(CarResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(carResources);
    }

    /// <summary>
    /// Updates an existing car certification in the system.
    /// </summary>
    /// <param name="carId">The ID of the car to update.</param>
    /// <param name="resource">The car update data.</param>
    /// <returns>The updated car resource if successful, BadRequest if update fails.</returns>
    [HttpPatch("{carId:int}")]
    public async Task<ActionResult<CarResource>> UpdateCar(int carId, [FromBody] UpdateCarResource resource)
    {
        try
        {
            Console.WriteLine($"Received UpdateCarResource for car {carId}: {System.Text.Json.JsonSerializer.Serialize(resource)}");
            
            var updateCarCommand = UpdateCarCommandFromResourceAssembler.ToCommandFromResource(resource, carId);
            var car = await carCommandService.Handle(updateCarCommand);
            
            if (car == null) 
            {
                return BadRequest(new { 
                    message = "Failed to update car certification.", 
                    details = "Check if car exists, brand ID is valid, license plate is unique, or validation requirements are met.",
                    validationRules = new {
                        year = "Must be between 1900 and current year + 1",
                        licensePlate = "Must be between 6 and 10 characters and unique",
                        price = "Must be non-negative",
                        pdfCertification = "Must be a valid Base64 string with at least 10 characters"
                    }
                });
            }
            
            var carResource = CarResourceFromEntityAssembler.ToResourceFromEntity(car);
            return Ok(carResource);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Validation error in UpdateCar: {ex.Message}");
            return BadRequest(new { 
                message = "Validation error", 
                details = ex.Message,
                parameter = ex.ParamName
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error in UpdateCar: {ex.Message}");
            return StatusCode(500, new { message = "Internal server error", details = ex.Message });
        }
    }

    [HttpGet("{id}/pdf")]
    public async Task<IActionResult> GetCarPdf(int id)
    {
        try
        {
            var getCarByIdQuery = new GetCarByIdQuery(id);
            var car = await carQueryService.Handle(getCarByIdQuery);
            
            if (car == null)
            {
                return NotFound(new { message = "Car not found" });
            }
            
            string pdfData = car.PdfCertification.Base64Data;
            if (!pdfData.StartsWith("data:"))
            {
                pdfData = $"data:application/pdf;base64,{pdfData}";
            }
            
            return Ok(new { pdfCertification = pdfData });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    /// <summary>
    /// Deletes a car certification from the system.
    /// </summary>
    /// <param name="carId">The ID of the car to delete.</param>
    /// <returns>NoContent if successful, NotFound if car doesn't exist.</returns>
    [HttpDelete("{carId:int}")]
    [SwaggerOperation(
        Summary = "Deletes a car certification",
        Description = "Deletes a car certification from the system",
        OperationId = "DeleteCar")]
    [SwaggerResponse(204, "The car was deleted successfully")]
    [SwaggerResponse(404, "The car was not found")]
    [SwaggerResponse(500, "Internal server error")]
    public async Task<IActionResult> DeleteCar(int carId)
    {
        try
        {
            var deleteCarCommand = new DeleteCarCommand(carId);
            var result = await carCommandService.Handle(deleteCarCommand);
            
            if (!result)
            {
                return NotFound(new { message = "Car not found" });
            }
            
            return NoContent();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting car with ID {carId}: {ex.Message}");
            return StatusCode(500, new { message = "Internal server error", details = ex.Message });
        }
    }
}