using CertiWeb.API.Reservation.Domain.Model.Aggregates;
using CertiWeb.API.Reservation.Domain.Model.Queries;
using ReservationEntity = CertiWeb.API.Reservation.Domain.Model.Aggregates.Reservation;

namespace CertiWeb.API.Reservation.Domain.Services;

/// <summary>
/// Service interface for handling reservation query operations.
/// </summary>
public interface IReservationQueryService
{
    /// <summary>
    /// Retrieves all reservations from the system.
    /// </summary>
    /// <param name="query">The query parameters for retrieving all reservations.</param>
    /// <returns>A collection of all reservations in the system.</returns>
    Task<IEnumerable<ReservationEntity>> Handle(GetAllReservationsQuery query);

    /// <summary>
    /// Retrieves a reservation by its unique identifier.
    /// </summary>
    /// <param name="query">The query containing the reservation ID to search for.</param>
    /// <returns>The reservation if found, null otherwise.</returns>
    Task<ReservationEntity?> Handle(GetReservationByIdQuery query);
    
    /// <summary>
    /// Retrieves all reservations made by a specific user.
    /// </summary>
    /// <param name="query">The query containing the user ID to search for.</param>
    /// <returns>A collection of reservations made by the user.</returns>
    Task<IEnumerable<ReservationEntity>> Handle(GetReservationsByUserIdQuery query);
    
    /// <summary>
    /// Retrieves all reservations with a specific status.
    /// </summary>
    /// <param name="query">The query containing the status to filter by.</param>
    /// <returns>A collection of reservations with the specified status.</returns>
    Task<IEnumerable<ReservationEntity>> Handle(GetReservationsByStatusQuery query);
}