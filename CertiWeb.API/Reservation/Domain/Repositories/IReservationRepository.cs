using CertiWeb.API.Shared.Domain.Repositories;
using CertiWeb.API.Reservation.Domain.Model.Aggregates;
using ReservationEntity = CertiWeb.API.Reservation.Domain.Model.Aggregates.Reservation;

namespace CertiWeb.API.Reservation.Domain.Repositories;

/// <summary>
/// Repository interface for reservation-related data operations.
/// </summary>
public interface IReservationRepository : IBaseRepository<ReservationEntity>
{
    /// <summary>
    /// Finds all reservations made by a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user whose reservations to retrieve.</param>
    /// <returns>A collection of reservations made by the user.</returns>
    Task<IEnumerable<ReservationEntity>> FindReservationsByUserIdAsync(int userId);
    
    /// <summary>
    /// Finds all reservations with a specific status.
    /// </summary>
    /// <param name="status">The status to filter by.</param>
    /// <returns>A collection of reservations with the specified status.</returns>
    Task<IEnumerable<ReservationEntity>> FindReservationsByStatusAsync(string status);
    
    /// <summary>
    /// Checks if a license plate already has a reservation for a specific date and time.
    /// </summary>
    /// <param name="licensePlate">The license plate to check.</param>
    /// <param name="inspectionDateTime">The inspection date and time.</param>
    /// <returns>True if a reservation exists, false otherwise.</returns>
    Task<bool> ExistsReservationForLicensePlateAndDateTimeAsync(string licensePlate, DateTime inspectionDateTime);
}