namespace CertiWeb.API.Reservation.Domain.Model.Commands;

/// <summary>
/// Command for deleting a reservation from the system.
/// </summary>
/// <param name="ReservationId">The ID of the reservation to delete.</param>
public record DeleteReservationCommand(int ReservationId);