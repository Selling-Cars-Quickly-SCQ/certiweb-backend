using System.ComponentModel.DataAnnotations;

namespace CertiWeb.API.Certifications.Domain.Model.Aggregates;

/// <summary>
/// Represents a vehicle brand entity.
/// </summary>
public class Brand
{
    /// <summary>
    /// Gets the unique identifier for the brand.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the brand name.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets whether the brand is active.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Initializes a new instance of the Brand class.
    /// </summary>
    public Brand()
    {
        Name = string.Empty;
        IsActive = true;
    }

    /// <summary>
    /// Initializes a new instance of the Brand class with the specified name.
    /// </summary>
    /// <param name="name">The brand name.</param>
    public Brand(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Brand name cannot be empty", nameof(name));
        
        Name = name;
        IsActive = true;
    }
}