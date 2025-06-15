using CertiWeb.API.Certifications.Domain.Model.Commands;
using CertiWeb.API.Certifications.Interfaces.REST.Resources;

namespace CertiWeb.API.Certifications.Interfaces.REST.Transform;

/// <summary>
/// Assembler to transform CreateCarResource to CreateCarCommand.
/// </summary>
public static class CreateCarCommandFromResourceAssembler
{
    /// <summary>
    /// Transforms a CreateCarResource to a CreateCarCommand.
    /// </summary>
    /// <param name="resource">The resource to transform.</param>
    /// <returns>The corresponding command.</returns>
    public static CreateCarCommand ToCommandFromResource(CreateCarResource resource)
    {
        return new CreateCarCommand(
            resource.Title,
            resource.Owner,
            resource.OwnerEmail,
            resource.Year,
            resource.BrandId,
            resource.Model,
            resource.Description,
            resource.PdfCertification,
            resource.ImageUrl,
            resource.Price,
            resource.LicensePlate,
            resource.OriginalReservationId
        );
    }
}