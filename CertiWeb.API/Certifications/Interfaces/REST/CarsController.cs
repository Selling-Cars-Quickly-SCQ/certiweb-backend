using System.Net.Mime;
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
        var createCarCommand = CreateCarCommandFromResourceAssembler.ToCommandFromResource(resource);
        var car = await carCommandService.Handle(createCarCommand);
        if (car == null) return BadRequest("Failed to create car certification. Check if reservation ID is already used or license plate already exists.");
        var carResource = CarResourceFromEntityAssembler.ToResourceFromEntity(car);

        return CreatedAtAction(nameof(GetCarById), new { carId = car.Id }, carResource);
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
}