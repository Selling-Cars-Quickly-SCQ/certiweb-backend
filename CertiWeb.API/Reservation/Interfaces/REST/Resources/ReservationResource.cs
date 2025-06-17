namespace CertiWeb.API.Reservation.Interfaces.REST.Resources;

/// <summary>
/// Resource representing a reservation in API responses.
/// </summary>
/// <param name="Id">The unique identifier for the reservation.</param>
/// <param name="UserId">The ID of the user who made the reservation.</param>
/// <param name="ReservationName">The name of the person making the reservation.</param>
/// <param name="ReservationEmail">The email of the person making the reservation.</param>
/// <param name="ImageUrl">The URL of the vehicle image.</param>
/// <param name="Brand">The vehicle brand.</param>
/// <param name="Model">The vehicle model.</param>
/// <param name="LicensePlate">The vehicle license plate (format XXX-XXX).</param>
/// <param name="InspectionDateTime">The inspection date and time.</param>
/// <param name="Price">The price for the inspection.</param>
/// <param name="Status">The reservation status.</param>
/// <param name="CreatedDate">The date when the reservation was created.</param>
/// <param name="UpdatedDate">The date when the reservation was last updated.</param>
public record ReservationResource(
    int Id,
    int UserId,
    string ReservationName,
    string ReservationEmail,
    string ImageUrl,
    string Brand,
    string Model,
    string LicensePlate,
    DateTime InspectionDateTime,
    string Price,
    string Status
);