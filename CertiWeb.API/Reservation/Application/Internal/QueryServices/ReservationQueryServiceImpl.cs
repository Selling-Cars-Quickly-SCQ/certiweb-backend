using CertiWeb.API.Reservation.Domain.Model.Aggregates;
using CertiWeb.API.Reservation.Domain.Model.Queries;
using CertiWeb.API.Reservation.Domain.Repositories;
using CertiWeb.API.Reservation.Domain.Services;
using ReservationEntity = CertiWeb.API.Reservation.Domain.Model.Aggregates.Reservation;

namespace CertiWeb.API.Reservation.Application.Internal.QueryServices;

/// <summary>
/// Implementation of the reservation query service.
/// </summary>
public class ReservationQueryServiceImpl : IReservationQueryService
{
    private readonly IReservationRepository _reservationRepository;

    /// <summary>
    /// Initializes a new instance of the ReservationQueryServiceImpl class.
    /// </summary>
    /// <param name="reservationRepository">The reservation repository.</param>
    public ReservationQueryServiceImpl(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    /// <summary>
    /// Retrieves all reservations from the system.
    /// </summary>
    /// <param name="query">The query parameters for retrieving all reservations.</param>
    /// <returns>A collection of all reservations in the system.</returns>
    public async Task<IEnumerable<ReservationEntity>> Handle(GetAllReservationsQuery query)
    {
        return await _reservationRepository.ListAsync();
    }

    /// <summary>
    /// Retrieves a reservation by its unique identifier.
    /// </summary>
    /// <param name="query">The query containing the reservation ID to search for.</param>
    /// <returns>The reservation if found, null otherwise.</returns>
    public async Task<ReservationEntity?> Handle(GetReservationByIdQuery query)
    {
        return await _reservationRepository.FindByIdAsync(query.ReservationId);
    }

    /// <summary>
    /// Retrieves all reservations made by a specific user.
    /// </summary>
    /// <param name="query">The query containing the user ID to search for.</param>
    /// <returns>A collection of reservations made by the user.</returns>
    public async Task<IEnumerable<ReservationEntity>> Handle(GetReservationsByUserIdQuery query)
    {
        return await _reservationRepository.FindReservationsByUserIdAsync(query.UserId);
    }

    /// <summary>
    /// Retrieves all reservations with a specific status.
    /// </summary>
    /// <param name="query">The query containing the status to filter by.</param>
    /// <returns>A collection of reservations with the specified status.</returns>
    public async Task<IEnumerable<ReservationEntity>> Handle(GetReservationsByStatusQuery query)
    {
        return await _reservationRepository.FindReservationsByStatusAsync(query.Status);
    }
}