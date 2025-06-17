namespace CertiWeb.API.Certifications.Domain.Model.Commands;

/// <summary>
/// Command for deleting a car certification from the system.
/// </summary>
/// <param name="Id">The ID of the car to delete.</param>
public record DeleteCarCommand(int Id);