namespace CertiWeb.API.Certifications.Domain.Model.Queries;

/// <summary>
/// Query for retrieving cars by owner email.
/// </summary>
/// <param name="OwnerEmail">The owner's email address.</param>
public record GetCarsByOwnerEmailQuery(string OwnerEmail);