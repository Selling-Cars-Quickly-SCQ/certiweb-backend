using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace CertiWeb.API.Reservation.Domain.Model.Aggregates;

/// <summary>
/// Partial class for Reservation that implements audit fields for tracking creation and update timestamps.
/// </summary>
public partial class Reservation : IEntityWithCreatedUpdatedDate
{
    /// <summary>
    /// Gets or sets the timestamp when the reservation was created.
    /// </summary>
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the reservation was last updated.
    /// </summary>
    [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }
}