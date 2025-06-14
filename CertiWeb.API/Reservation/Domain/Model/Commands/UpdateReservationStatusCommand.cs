namespace CertiWeb.API.Reservation.Domain.Model.Commands;

/// <summary>
/// Command for updating the status of a reservation.
/// </summary>
/// <param name="ReservationId">The ID of the reservation to update.</param>
/// <param name="Status">The new status (pending, accepted, rejected).</param>
public record UpdateReservationStatusCommand(int ReservationId, string Status);