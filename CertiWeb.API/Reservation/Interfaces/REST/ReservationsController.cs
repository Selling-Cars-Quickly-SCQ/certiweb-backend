using System.Net.Mime;
using CertiWeb.API.Reservation.Domain.Model.Commands;
using CertiWeb.API.Reservation.Domain.Model.Queries;
using CertiWeb.API.Reservation.Domain.Services;
using CertiWeb.API.Reservation.Interfaces.REST.Resources;
using CertiWeb.API.Reservation.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CertiWeb.API.Reservation.Interfaces.REST;

/// <summary>
/// Controller for managing reservation operations.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ReservationsController : ControllerBase
{
    private readonly IReservationCommandService _reservationCommandService;
    private readonly IReservationQueryService _reservationQueryService;

    /// <summary>
    /// Initializes a new instance of the ReservationsController class.
    /// </summary>
    /// <param name="reservationCommandService">The reservation command service.</param>
    /// <param name="reservationQueryService">The reservation query service.</param>
    public ReservationsController(IReservationCommandService reservationCommandService, IReservationQueryService reservationQueryService)
    {
        _reservationCommandService = reservationCommandService;
        _reservationQueryService = reservationQueryService;
    }

    /// <summary>
    /// Creates a new reservation.
    /// </summary>
    /// <param name="resource">The reservation data.</param>
    /// <returns>The created reservation.</returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Creates a new reservation",
        Description = "Creates a new reservation with the provided data",
        OperationId = "CreateReservation")]
    [SwaggerResponse(201, "The reservation was created", typeof(ReservationResource))]
    [SwaggerResponse(400, "The reservation data is invalid")]
    public async Task<IActionResult> CreateReservation([FromBody] CreateReservationResource resource)
    {
        var createReservationCommand = CreateReservationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var reservation = await _reservationCommandService.Handle(createReservationCommand);
        if (reservation is null) return BadRequest();
        var reservationResource = ReservationResourceFromEntityAssembler.ToResourceFromEntity(reservation);
        return CreatedAtAction(nameof(GetReservationById), new { reservationId = reservation.Id }, reservationResource);
    }

    /// <summary>
    /// Gets all reservations.
    /// </summary>
    /// <returns>A collection of all reservations.</returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Gets all reservations",
        Description = "Gets all reservations in the system",
        OperationId = "GetAllReservations")]
    [SwaggerResponse(200, "Returns all reservations", typeof(IEnumerable<ReservationResource>))]
    public async Task<IActionResult> GetAllReservations()
    {
        var getAllReservationsQuery = new GetAllReservationsQuery();
        var reservations = await _reservationQueryService.Handle(getAllReservationsQuery);
        var reservationResources = reservations.Select(ReservationResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(reservationResources);
    }

    /// <summary>
    /// Gets a reservation by its ID.
    /// </summary>
    /// <param name="reservationId">The reservation ID.</param>
    /// <returns>The reservation if found.</returns>
    [HttpGet("{reservationId}")]
    [SwaggerOperation(
        Summary = "Gets a reservation by id",
        Description = "Gets a reservation for the given identifier",
        OperationId = "GetReservationById")]
    [SwaggerResponse(200, "Returns the reservation", typeof(ReservationResource))]
    [SwaggerResponse(404, "The reservation was not found")]
    public async Task<IActionResult> GetReservationById([FromRoute] int reservationId)
    {
        var getReservationByIdQuery = new GetReservationByIdQuery(reservationId);
        var reservation = await _reservationQueryService.Handle(getReservationByIdQuery);
        if (reservation == null) return NotFound();
        var reservationResource = ReservationResourceFromEntityAssembler.ToResourceFromEntity(reservation);
        return Ok(reservationResource);
    }

    /// <summary>
    /// Gets all reservations for a specific user.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>A collection of reservations for the user.</returns>
    [HttpGet("user/{userId}")]
    [SwaggerOperation(
        Summary = "Gets reservations by user id",
        Description = "Gets all reservations for the given user identifier",
        OperationId = "GetReservationsByUserId")]
    [SwaggerResponse(200, "Returns the user's reservations", typeof(IEnumerable<ReservationResource>))]
    public async Task<IActionResult> GetReservationsByUserId([FromRoute] int userId)
    {
        var getReservationsByUserIdQuery = new GetReservationsByUserIdQuery(userId);
        var reservations = await _reservationQueryService.Handle(getReservationsByUserIdQuery);
        var reservationResources = reservations.Select(ReservationResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(reservationResources);
    }

    /// <summary>
    /// Gets all reservations with a specific status.
    /// </summary>
    /// <param name="status">The reservation status.</param>
    /// <returns>A collection of reservations with the specified status.</returns>
    [HttpGet("status/{status}")]
    [SwaggerOperation(
        Summary = "Gets reservations by status",
        Description = "Gets all reservations with the given status",
        OperationId = "GetReservationsByStatus")]
    [SwaggerResponse(200, "Returns reservations with the specified status", typeof(IEnumerable<ReservationResource>))]
    public async Task<IActionResult> GetReservationsByStatus([FromRoute] string status)
    {
        var getReservationsByStatusQuery = new GetReservationsByStatusQuery(status);
        var reservations = await _reservationQueryService.Handle(getReservationsByStatusQuery);
        var reservationResources = reservations.Select(ReservationResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(reservationResources);
    }

    /// <summary>
    /// Updates the status of a reservation.
    /// </summary>
    /// <param name="reservationId">The reservation ID.</param>
    /// <param name="status">The new status.</param>
    /// <returns>The updated reservation.</returns>
    [HttpPut("{reservationId}/status")]
    [SwaggerOperation(
        Summary = "Updates reservation status",
        Description = "Updates the status of the given reservation",
        OperationId = "UpdateReservationStatus")]
    [SwaggerResponse(200, "The reservation status was updated", typeof(ReservationResource))]
    [SwaggerResponse(404, "The reservation was not found")]
    public async Task<IActionResult> UpdateReservationStatus([FromRoute] int reservationId, [FromBody] string status)
    {
        var updateReservationStatusCommand = new UpdateReservationStatusCommand(reservationId, status);
        var reservation = await _reservationCommandService.Handle(updateReservationStatusCommand);
        if (reservation == null) return NotFound();
        var reservationResource = ReservationResourceFromEntityAssembler.ToResourceFromEntity(reservation);
        return Ok(reservationResource);
    }

    /// <summary>
    /// Updates a reservation.
    /// </summary>
    /// <param name="reservationId">The reservation ID.</param>
    /// <param name="resource">The updated reservation data.</param>
    /// <returns>The updated reservation.</returns>
    [HttpPut("{reservationId}")]
    [SwaggerOperation(
        Summary = "Updates a reservation",
        Description = "Updates a reservation with the provided data",
        OperationId = "UpdateReservation")]
    [SwaggerResponse(200, "The reservation was updated", typeof(ReservationResource))]
    [SwaggerResponse(404, "The reservation was not found")]
    [SwaggerResponse(400, "The reservation data is invalid")]
    public async Task<IActionResult> UpdateReservation([FromRoute] int reservationId, [FromBody] UpdateReservationResource resource)
    {
        var updateReservationStatusCommand = new UpdateReservationStatusCommand(reservationId, resource.Status);
        var reservation = await _reservationCommandService.Handle(updateReservationStatusCommand);
        if (reservation == null) return NotFound();
        var reservationResource = ReservationResourceFromEntityAssembler.ToResourceFromEntity(reservation);
        return Ok(reservationResource);
    }

    /// <summary>
    /// Deletes a reservation from the system.
    /// </summary>
    /// <param name="reservationId">The ID of the reservation to delete.</param>
    /// <returns>NoContent if successful, NotFound if reservation doesn't exist.</returns>
    [HttpDelete("{reservationId}")]
    [SwaggerOperation(
        Summary = "Deletes a reservation",
        Description = "Deletes a reservation from the system",
        OperationId = "DeleteReservation")]
    [SwaggerResponse(204, "The reservation was deleted successfully")]
    [SwaggerResponse(404, "The reservation was not found")]
    [SwaggerResponse(500, "Internal server error")]
    public async Task<IActionResult> DeleteReservation([FromRoute] int reservationId)
    {
        try
        {
            var deleteReservationCommand = new DeleteReservationCommand(reservationId);
            var result = await _reservationCommandService.Handle(deleteReservationCommand);
            
            if (!result)
            {
                return NotFound(new { message = "Reservation not found" });
            }
            
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", details = ex.Message });
        }
    }
}