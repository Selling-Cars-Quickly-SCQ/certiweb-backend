using CertiWeb.API.Certifications.Domain.Model.Commands;
using CertiWeb.API.Certifications.Interfaces.REST.Resources;

namespace CertiWeb.API.Certifications.Interfaces.REST.Transform;

/// <summary>
/// Assembler to transform UpdateCarResource to UpdateCarCommand.
/// </summary>
public static class UpdateCarCommandFromResourceAssembler
{
    /// <summary>
    /// Transforms an UpdateCarResource to an UpdateCarCommand.
    /// </summary>
    /// <param name="resource">The resource to transform.</param>
    /// <param name="carId">The ID of the car to update.</param>
    /// <returns>The corresponding command.</returns>
    public static UpdateCarCommand ToCommandFromResource(UpdateCarResource resource, int carId)
    {
        return new UpdateCarCommand(
            carId,
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
            resource.LicensePlate
        );
    }
}