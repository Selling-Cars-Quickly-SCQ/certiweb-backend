namespace CertiWeb.API.Reservation.Domain.Model.Queries;

/// <summary>
/// Query for retrieving reservations by their status.
/// </summary>
/// <param name="Status">The status to filter by (pending, accepted, rejected).</param>
public record GetReservationsByStatusQuery(string Status);