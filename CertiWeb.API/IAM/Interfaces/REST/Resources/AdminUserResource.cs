namespace CertiWeb.API.IAM.Interfaces.REST.Resources;

/// <summary>
/// Resource representation of AdminUser for API responses
/// </summary>
public class AdminUserResource
{
    /// <summary>
    /// Admin user unique identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Admin user name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Admin user email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Admin user hashed password
    /// </summary>
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