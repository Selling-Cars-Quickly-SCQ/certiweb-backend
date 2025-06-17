using System.Net.Mime;
using CertiWeb.API.Certifications.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CertiWeb.API.Certifications.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Brand Endpoints.")]
public class BrandsController(IBrandRepository brandRepository) : ControllerBase
{
    /// <summary>
    /// Retrieves all active brands.
    /// </summary>
    /// <returns>A collection of all active brands.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetAllBrands()
    {
        var brands = await brandRepository.GetActiveBrandsAsync();
        var brandResources = brands.Select(b => new { Id = b.Id, Name = b.Name });
        return Ok(brandResources);
    }
}