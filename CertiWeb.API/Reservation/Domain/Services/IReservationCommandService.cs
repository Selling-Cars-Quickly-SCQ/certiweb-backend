using CertiWeb.API.Reservation.Domain.Model.Aggregates;
using CertiWeb.API.Reservation.Domain.Model.Commands;
using ReservationEntity = CertiWeb.API.Reservation.Domain.Model.Aggregates.Reservation;

namespace CertiWeb.API.Reservation.Domain.Services;

/// <summary>
/// Service interface for handling reservation command operations.
/// </summary>
public interface IReservationCommandService
{
    /// <summary>
    /// Creates a new reservation in the system.
    /// </summary>
    /// <param name="command">The command containing the reservation data.</param>
    /// <returns>The created reservation.</returns>
    Task<ReservationEntity?> Handle(CreateReservationCommand command);
    
    /// <summary>
    /// Updates the status of an existing reservation.
    /// </summary>
    /// <param name="command">The command containing the reservation ID and new status.</param>
    /// <returns>The updated reservation.</returns>
    Task<ReservationEntity?> Handle(UpdateReservationStatusCommand command);
}