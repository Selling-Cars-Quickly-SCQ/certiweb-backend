using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using CertiWeb.API.Reservation.Domain.Model.Commands;

namespace CertiWeb.API.Reservation.Domain.Model.Aggregates;

/// <summary>
/// Represents a reservation entity in the system.
/// </summary>
public partial class Reservation
{
    /// <summary>
    /// Gets the unique identifier for the reservation.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets or sets the user ID who made the reservation.
    /// </summary>
    public required int UserId { get; set; }

    /// <summary>
    /// Gets or sets the name of the person making the reservation.
    /// </summary>
    public required string ReservationName { get; set; }

    /// <summary>
    /// Gets or sets the email of the person making the reservation.
    /// </summary>
    public required string ReservationEmail { get; set; }

    /// <summary>
    /// Gets or sets the URL of the vehicle image.
    /// </summary>
    public required string ImageUrl { get; set; }

    /// <summary>
    /// Gets or sets the vehicle brand.
    /// </summary>
    public required string Brand { get; set; }

    /// <summary>
    /// Gets or sets the vehicle model.
    /// </summary>
    public required string Model { get; set; }

    /// <summary>
    /// Gets or sets the vehicle license plate (format XXX-XXX: 3 alphanumeric characters, hyphen, 3 alphanumeric characters).
    /// </summary>
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Gets or sets the inspection date and time.
    /// Available times: 9:00AM, 11:00AM, 1:00PM, 3:00PM, 5:00PM (Peru time).
    /// </summary>
    public required DateTime InspectionDateTime { get; set; }

    /// <summary>
    /// Gets or sets the price for the inspection.
    /// </summary>
    public required string Price { get; set; }

    /// <summary>
    /// Gets or sets the reservation status (pending, accepted, rejected).
    /// </summary>
    public required string Status { get; set; }

    /// <summary>
    /// Initializes a new instance of the Reservation class.
    /// This parameterless constructor is required by Entity Framework Core.
    /// </summary>
    public Reservation()
    {
        UserId = 0;
        ReservationName = string.Empty;
        ReservationEmail = string.Empty;
        ImageUrl = string.Empty;
        Brand = string.Empty;
        Model = string.Empty;
        LicensePlate = string.Empty;
        InspectionDateTime = DateTime.MinValue;
        Price = string.Empty;
        Status = "pending";
    }

    /// <summary>
    /// Initializes a new instance of the Reservation class with the specified creation command.
    /// </summary>
    /// <param name="command">The command containing the reservation's initial data.</param>
    [SetsRequiredMembers]
    public Reservation(CreateReservationCommand command)
    {
        UserId = command.UserId;
        ReservationName = command.ReservationName;
        ReservationEmail = command.ReservationEmail;
        ImageUrl = command.ImageUrl;
        Brand = command.Brand;
        Model = command.Model;
        LicensePlate = FormatLicensePlate(command.LicensePlate);
        InspectionDateTime = command.InspectionDateTime;
        Price = command.Price;
        Status = "pending";
    }

    /// <summary>
    /// Formats the license plate to ensure proper XXX-XXX format.
    /// </summary>
    /// <param name="licensePlate">The license plate to format.</param>
    /// <returns>The formatted license plate in XXX-XXX format.</returns>
    private static string FormatLicensePlate(string licensePlate)
    {
        if (string.IsNullOrWhiteSpace(licensePlate))
            return string.Empty;
            
        var cleanPlate = licensePlate.Replace("-", "").ToUpper();
        if (cleanPlate.Length == 6)
        {
            return $"{cleanPlate.Substring(0, 3)}-{cleanPlate.Substring(3, 3)}";
        }
        return licensePlate.ToUpper();
    }
}