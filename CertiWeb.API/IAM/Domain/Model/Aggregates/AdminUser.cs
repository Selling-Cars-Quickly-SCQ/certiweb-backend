using System.ComponentModel.DataAnnotations;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace CertiWeb.API.IAM.Domain.Model.Aggregates;

/// <summary>
/// Admin User entity for administrative authentication
/// </summary>
public class AdminUser : IEntityWithCreatedUpdatedDate
{
    /// <summary>
    /// Admin User unique identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Admin user name
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Admin user email address (unique)
    /// </summary>
    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Admin user password (stored as hashed password using BCrypt)
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Date when the admin user was created
    /// </summary>
    public DateTimeOffset? CreatedDate { get; set; }

    /// <summary>
    /// Date when the admin user was last updated
    /// </summary>
    public DateTimeOffset? UpdatedDate { get; set; }
}