namespace CertiWeb.API.Reservation.Domain.Model.Queries;

/// <summary>
/// Query for retrieving a reservation by its unique identifier.
/// </summary>
/// <param name="ReservationId">The ID of the reservation to retrieve.</param>
public record GetReservationByIdQuery(int ReservationId);