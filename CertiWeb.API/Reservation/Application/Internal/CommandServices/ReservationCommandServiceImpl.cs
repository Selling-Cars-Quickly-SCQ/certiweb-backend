using CertiWeb.API.Reservation.Domain.Model.Aggregates;
using CertiWeb.API.Reservation.Domain.Model.Commands;
using CertiWeb.API.Reservation.Domain.Repositories;
using CertiWeb.API.Reservation.Domain.Services;
using CertiWeb.API.Shared.Domain.Repositories;
using ReservationEntity = CertiWeb.API.Reservation.Domain.Model.Aggregates.Reservation;

namespace CertiWeb.API.Reservation.Application.Internal.CommandServices;

/// <summary>
/// Implementation of the reservation command service.
/// </summary>
public class ReservationCommandServiceImpl : IReservationCommandService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the ReservationCommandServiceImpl class.
    /// </summary>
    /// <param name="reservationRepository">The reservation repository.</param>
    /// <param name="unitOfWork">The unit of work for transaction management.</param>
    public ReservationCommandServiceImpl(IReservationRepository reservationRepository, IUnitOfWork unitOfWork)
    {
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates a new reservation in the system.
    /// </summary>
    /// <param name="command">The command containing the reservation data.</param>
    /// <returns>The created reservation.</returns>
    public async Task<ReservationEntity?> Handle(CreateReservationCommand command)
    {
        // Validate license plate format (max 6 characters, uppercase letters and numbers)
        if (string.IsNullOrWhiteSpace(command.LicensePlate) || 
            command.LicensePlate.Length > 6 || 
            !command.LicensePlate.All(c => char.IsLetterOrDigit(c)))
        {
            throw new ArgumentException("License plate must be max 6 characters with letters and numbers only.");
        }

        // Check if license plate already has a reservation for the same date/time
        var existingReservation = await _reservationRepository
            .ExistsReservationForLicensePlateAndDateTimeAsync(command.LicensePlate, command.InspectionDateTime);
        
        if (existingReservation)
        {
            throw new InvalidOperationException("A reservation already exists for this license plate at the specified date and time.");
        }

        // Validate inspection time slots (9:00AM, 11:00AM, 1:00PM, 3:00PM, 5:00PM)
        var validHours = new[] { 9, 11, 13, 15, 17 };
        if (!validHours.Contains(command.InspectionDateTime.Hour) || 
            command.InspectionDateTime.Minute != 0 || 
            command.InspectionDateTime.Second != 0)
        {
            throw new ArgumentException("Inspection time must be one of: 9:00AM, 11:00AM, 1:00PM, 3:00PM, 5:00PM.");
        }

        var reservation = new ReservationEntity(command);
        
        try
        {
            await _reservationRepository.AddAsync(reservation);
            await _unitOfWork.CompleteAsync();
            return reservation;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Updates the status of an existing reservation.
    /// </summary>
    /// <param name="command">The command containing the reservation ID and new status.</param>
    /// <returns>The updated reservation.</returns>
    public async Task<ReservationEntity?> Handle(UpdateReservationStatusCommand command)
    {
        var reservation = await _reservationRepository.FindByIdAsync(command.ReservationId);
        if (reservation == null) return null;

        // Validate status values
        var validStatuses = new[] { "pending", "accepted", "rejected" };
        if (!validStatuses.Contains(command.Status.ToLower()))
        {
            throw new ArgumentException("Status must be one of: pending, accepted, rejected.");
        }

        reservation.Status = command.Status.ToLower();
        
        try
        {
            _reservationRepository.Update(reservation);
            await _unitOfWork.CompleteAsync();
            return reservation;
        }
        catch (Exception)
        {
            return null;
        }
    }
}