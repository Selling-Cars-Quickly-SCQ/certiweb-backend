namespace CertiWeb.API.Reservation.Interfaces.REST.Resources;

/// <summary>
/// Resource for updating a reservation.
/// </summary>
/// <param name="Status">The new reservation status.</param>
public record UpdateReservationResource(
    string Status
);