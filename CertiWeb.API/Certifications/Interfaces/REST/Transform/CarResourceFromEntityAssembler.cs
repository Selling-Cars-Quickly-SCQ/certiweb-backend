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
        return new CarResource(
            entity.Id,
            entity.Title,
            entity.Owner,
            entity.OwnerEmail,
            entity.Year.Value,
            entity.Brand?.Name ?? "Unknown",
            entity.Model,
            entity.Description,
            entity.PdfCertification.Base64Data,
            entity.ImageUrl,
            entity.Price.Value,
            entity.LicensePlate.Value,
            entity.OriginalReservationId
        );
    }
}