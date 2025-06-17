namespace CertiWeb.API.Certifications.Domain.Model.Queries;

/// <summary>
/// Query for retrieving a car by its ID.
/// </summary>
/// <param name="Id">The car ID.</param>
public record GetCarByIdQuery(int Id);