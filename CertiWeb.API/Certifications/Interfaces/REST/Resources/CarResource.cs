namespace CertiWeb.API.Certifications.Interfaces.REST.Resources;

/// <summary>
/// Data transfer object for representing a car in the REST API responses.
/// </summary>
/// <param name="Id">The unique identifier of the car.</param>
/// <param name="Title">The title of the car.</param>
/// <param name="Owner">The owner's name.</param>
/// <param name="OwnerEmail">The owner's email address.</param>
/// <param name="Year">The car's year.</param>
/// <param name="Brand">The brand name.</param>
/// <param name="Model">The car model.</param>
/// <param name="Description">The car description.</param>
/// <param name="PdfCertification">The PDF certification as Base64.</param>
/// <param name="ImageUrl">The car image URL.</param>
/// <param name="Price">The car price.</param>
/// <param name="LicensePlate">The license plate.</param>
/// <param name="OriginalReservationId">The original reservation ID.</param>
/// <param name="CreatedAt">The creation date.</param>
/// <param name="UpdatedAt">The last update date.</param>
public record CarResource(
    int Id,
    string Title,
    string Owner,
    string OwnerEmail,
    int Year,
    string Brand,
    string Model,
    string Description,
    string PdfCertification,
    string ImageUrl,
    decimal Price,
    string LicensePlate,
    int OriginalReservationId
);