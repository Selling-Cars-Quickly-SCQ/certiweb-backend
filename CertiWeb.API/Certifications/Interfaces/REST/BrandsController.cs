using System.Net.Mime;
using CertiWeb.API.Certifications.Application.Internal.QueryServices;
using CertiWeb.API.Certifications.Interfaces.REST.Resources;
using CertiWeb.API.Certifications.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CertiWeb.API.Certifications.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Brand Endpoints.")]
/// <summary>
/// REST API controller for managing brand operations.
/// </summary>
public class BrandsController(BrandQueryServiceImpl brandQueryService) : ControllerBase
{
    /// <summary>
    /// Retrieves all active brands from the system.
    /// </summary>
    /// <returns>A collection of all active brand resources.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BrandResource>>> GetAllActiveBrands()
    {
        var brands = await brandQueryService.GetAllActiveBrandsAsync();
        var brandResources = brands.Select(BrandResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(brandResources);
    }

    /// <summary>
    /// Retrieves a specific brand by its ID.
    /// </summary>
    /// <param name="brandId">The ID of the brand to retrieve.</param>
    /// <returns>The brand resource if found, NotFound if the brand doesn't exist.</returns>
    [HttpGet("{brandId:int}")]
    public async Task<ActionResult<BrandResource>> GetBrandById(int brandId)
    {
        var brand = await brandQueryService.GetBrandByIdAsync(brandId);
        if (brand == null) return NotFound();
        var brandResource = BrandResourceFromEntityAssembler.ToResourceFromEntity(brand);
        return Ok(brandResource);
    }
}