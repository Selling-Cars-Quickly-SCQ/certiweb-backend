namespace CertiWeb.API.Certifications.Interfaces.REST.Resources;

/// <summary>
/// Data transfer object for creating a new car certification.
/// </summary>
/// <param name="Title">The title of the car.</param>
/// <param name="Owner">The owner's name.</param>
/// <param name="OwnerEmail">The owner's email address.</param>
/// <param name="Year">The car's year.</param>
/// <param name="BrandId">The brand ID.</param>
/// <param name="Model">The car model.</param>
/// <param name="Description">The car description.</param>
/// <param name="PdfCertification">The PDF certification as Base64.</param>
/// <param name="ImageUrl">The car image URL.</param>
/// <param name="Price">The car price.</param>
/// <param name="LicensePlate">The license plate.</param>
/// <param name="OriginalReservationId">The original reservation ID.</param>
public record CreateCarResource(
    string Title,
    string Owner,
    string OwnerEmail,
    int Year,
    int BrandId,
    string Model,
    string? Description,
    string PdfCertification,
    string? ImageUrl,
    decimal Price,
    string LicensePlate,
    int OriginalReservationId
);