using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace CertiWeb.API.Users.Domain.Model.Aggregates;

/// <summary>
/// Partial class for User that implements audit fields for tracking creation and update timestamps.
/// </summary>
public partial class User : IEntityWithCreatedUpdatedDate
{
    /// <summary>
    /// Gets or sets the timestamp when the user was created.
    /// </summary>
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the user was last updated.
    /// </summary>
    [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }
}