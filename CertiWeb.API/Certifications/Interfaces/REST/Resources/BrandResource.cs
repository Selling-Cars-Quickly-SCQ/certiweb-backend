namespace CertiWeb.API.Certifications.Interfaces.REST.Resources;

/// <summary>
/// Data transfer object for representing a brand in the REST API responses.
/// </summary>
/// <param name="Id">The unique identifier of the brand.</param>
/// <param name="Name">The brand name.</param>
/// <param name="IsActive">Whether the brand is active.</param>
public record BrandResource(int Id, string Name, bool IsActive);