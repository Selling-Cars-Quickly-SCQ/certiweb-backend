namespace CertiWeb.API.Reservation.Domain.Model.Queries;

/// <summary>
/// Query for retrieving all reservations made by a specific user.
/// </summary>
/// <param name="UserId">The ID of the user whose reservations to retrieve.</param>
public record GetReservationsByUserIdQuery(int UserId);