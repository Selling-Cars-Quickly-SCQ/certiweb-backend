namespace CertiWeb.API.Reservation.Interfaces.REST.Resources;

/// <summary>
/// Resource for creating a new reservation.
/// </summary>
/// <param name="UserId">The ID of the user making the reservation.</param>
/// <param name="ReservationName">The name of the person making the reservation.</param>
/// <param name="ReservationEmail">The email of the person making the reservation.</param>
/// <param name="ImageUrl">The URL of the vehicle image.</param>
/// <param name="Brand">The vehicle brand.</param>
/// <param name="Model">The vehicle model.</param>
/// <param name="LicensePlate">The vehicle license plate (format XXX-XXX).</param>
/// <param name="InspectionDateTime">The inspection date and time.</param>
/// <param name="Price">The price for the inspection.</param>
public record CreateReservationResource(
    int UserId,
    string ReservationName,
    string ReservationEmail,
    string ImageUrl,
    string Brand,
    string Model,
    string LicensePlate,
    DateTime InspectionDateTime,
    string Price
);