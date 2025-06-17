using CertiWeb.API.Certifications.Domain.Model.Aggregates;
using CertiWeb.API.Certifications.Interfaces.REST.Resources;

namespace CertiWeb.API.Certifications.Interfaces.REST.Transform;

/// <summary>
/// Assembler to transform Car entity to CarResource.
/// </summary>
public static class CarResourceFromEntityAssembler
{
    /// <summary>
    /// Transforms a Car entity to a CarResource.
    /// </summary>
    /// <param name="entity">The entity to transform.</param>
    /// <returns>The corresponding resource.</returns>
    public static CarResource ToResourceFromEntity(Car entity)
    {
        return new CarResource
        {
            Id = entity.Id,
            Title = entity.Title ?? string.Empty,
            Owner = entity.Owner ?? string.Empty,
            OwnerEmail = entity.OwnerEmail ?? string.Empty,
            Year = entity.Year,
            BrandId = entity.BrandId,
            Brand = entity.Brand?.Name ?? string.Empty,
            Model = entity.Model ?? string.Empty,
            Description = entity.Description ?? string.Empty,
            ImageUrl = entity.ImageUrl ?? string.Empty,
            Price = entity.Price,
            LicensePlate = entity.LicensePlate ?? string.Empty,
            OriginalReservationId = entity.OriginalReservationId,
            HasPdfCertification = !string.IsNullOrEmpty(entity.PdfCertification?.Base64Data)
        };
    }
}