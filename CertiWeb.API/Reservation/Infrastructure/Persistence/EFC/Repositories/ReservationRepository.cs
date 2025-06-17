using CertiWeb.API.Reservation.Domain.Model.Aggregates;
using CertiWeb.API.Reservation.Domain.Repositories;
using CertiWeb.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CertiWeb.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using ReservationEntity = CertiWeb.API.Reservation.Domain.Model.Aggregates.Reservation;

namespace CertiWeb.API.Reservation.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Repository implementation for reservation-related data operations using Entity Framework Core.
/// </summary>
public class ReservationRepository : BaseRepository<ReservationEntity>, IReservationRepository
{
    /// <summary>
    /// Initializes a new instance of the ReservationRepository class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public ReservationRepository(AppDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Finds all reservations made by a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user whose reservations to retrieve.</param>
    /// <returns>A collection of reservations made by the user.</returns>
    public async Task<IEnumerable<ReservationEntity>> FindReservationsByUserIdAsync(int userId)
    {
        return await Context.Set<ReservationEntity>()
            .Where(r => r.UserId == userId)
            .ToListAsync();
    }

    /// <summary>
    /// Finds all reservations with a specific status.
    /// </summary>
    /// <param name="status">The status to filter by.</param>
    /// <returns>A collection of reservations with the specified status.</returns>
    public async Task<IEnumerable<ReservationEntity>> FindReservationsByStatusAsync(string status)
    {
        return await Context.Set<ReservationEntity>()
            .Where(r => r.Status.ToLower() == status.ToLower())
            .ToListAsync();
    }

    /// <summary>
    /// Checks if a license plate already has a reservation for a specific date and time.
    /// </summary>
    /// <param name="licensePlate">The license plate to check.</param>
    /// <param name="inspectionDateTime">The inspection date and time.</param>
    /// <returns>True if a reservation exists, false otherwise.</returns>
    public async Task<bool> ExistsReservationForLicensePlateAndDateTimeAsync(string licensePlate, DateTime inspectionDateTime)
    {
        var cleanLicensePlate = licensePlate.Replace("-", "").ToUpper();
        return await Context.Set<ReservationEntity>()
            .AnyAsync(r => r.LicensePlate.Replace("-", "").ToUpper() == cleanLicensePlate &&
                          r.InspectionDateTime.Date == inspectionDateTime.Date &&
                          r.InspectionDateTime.Hour == inspectionDateTime.Hour);
    }
}