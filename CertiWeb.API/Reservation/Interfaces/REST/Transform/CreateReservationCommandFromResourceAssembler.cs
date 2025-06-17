using CertiWeb.API.Reservation.Domain.Model.Commands;
using CertiWeb.API.Reservation.Interfaces.REST.Resources;

namespace CertiWeb.API.Reservation.Interfaces.REST.Transform;

/// <summary>
/// Assembler to transform CreateReservationResource to CreateReservationCommand.
/// </summary>
public static class CreateReservationCommandFromResourceAssembler
{
    /// <summary>
    /// Transforms a CreateReservationResource to a CreateReservationCommand.
    /// </summary>
    /// <param name="resource">The resource to transform.</param>
    /// <returns>The corresponding command.</returns>
    public static CreateReservationCommand ToCommandFromResource(CreateReservationResource resource)
    {
        return new CreateReservationCommand(
            resource.UserId,
            resource.ReservationName,
            resource.ReservationEmail,
            resource.ImageUrl,
            resource.Brand,
            resource.Model,
            resource.LicensePlate,
            resource.InspectionDateTime,
            resource.Price
        );
    }
}