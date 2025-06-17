using CertiWeb.API.Certifications.Domain.Model.Aggregates;
using CertiWeb.API.Certifications.Interfaces.REST.Resources;

namespace CertiWeb.API.Certifications.Interfaces.REST.Transform;

/// <summary>
/// Assembler to transform Brand entity to BrandResource.
/// </summary>
public static class BrandResourceFromEntityAssembler
{
    /// <summary>
    /// Transforms a Brand entity to a BrandResource.
    /// </summary>
    /// <param name="entity">The entity to transform.</param>
    /// <returns>The corresponding resource.</returns>
    public static BrandResource ToResourceFromEntity(Brand entity)
    {
        return new BrandResource(
            entity.Id,
            entity.Name,
            entity.IsActive
        );
    }
}