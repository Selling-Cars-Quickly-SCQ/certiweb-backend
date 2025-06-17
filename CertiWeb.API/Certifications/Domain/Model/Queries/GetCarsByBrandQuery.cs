namespace CertiWeb.API.Certifications.Domain.Model.Queries;

/// <summary>
/// Query for retrieving cars by brand.
/// </summary>
/// <param name="BrandId">The brand ID.</param>
public record GetCarsByBrandQuery(int BrandId);