using CertiWeb.API.Reservation.Domain.Model.Aggregates;
using CertiWeb.API.Reservation.Interfaces.REST.Resources;
using ReservationEntity = CertiWeb.API.Reservation.Domain.Model.Aggregates.Reservation;

namespace CertiWeb.API.Reservation.Interfaces.REST.Transform;

/// <summary>
/// Assembler to transform Reservation entity to ReservationResource.
/// </summary>
public static class ReservationResourceFromEntityAssembler
{
    /// <summary>
    /// Transforms a Reservation entity to a ReservationResource.
    /// </summary>
    /// <param name="entity">The entity to transform.</param>
    /// <returns>The corresponding resource.</returns>
    public static ReservationResource ToResourceFromEntity(ReservationEntity entity)
    {
        return new ReservationResource(
            entity.Id,
            entity.UserId,
            entity.ReservationName,
            entity.ReservationEmail,
            entity.ImageUrl,
            entity.Brand,
            entity.Model,
            entity.LicensePlate,
            entity.InspectionDateTime,
            entity.Price,
            entity.Status
        );
    }
}