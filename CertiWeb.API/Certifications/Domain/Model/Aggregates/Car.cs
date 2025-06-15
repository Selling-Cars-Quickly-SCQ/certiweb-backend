using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using CertiWeb.API.Certifications.Domain.Model.Commands;
using CertiWeb.API.Certifications.Domain.Model.ValueObjects;

namespace CertiWeb.API.Certifications.Domain.Model.Aggregates;

/// <summary>
/// Represents a car entity in the certification system.
/// </summary>
public class Car
{
    /// <summary>
    /// Gets the unique identifier for the car.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Gets or sets the car title.
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the owner's name.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Owner { get; set; }

    /// <summary>
    /// Gets or sets the owner's email.
    /// </summary>
    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string OwnerEmail { get; set; }

    /// <summary>
    /// Gets or sets the car year.
    /// </summary>
    public required Year Year { get; set; }

    /// <summary>
    /// Gets or sets the brand ID.
    /// </summary>
    public int BrandId { get; set; }

    /// <summary>
    /// Gets or sets the brand navigation property.
    /// </summary>
    public Brand Brand { get; set; }

    /// <summary>
    /// Gets or sets the car model.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Model { get; set; }

    /// <summary>
    /// Gets or sets the car description.
    /// </summary>
    [MaxLength(1000)]
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the PDF certification as Base64.
    /// </summary>
    [Column(TypeName = "TEXT")]
    public required PdfCertification PdfCertification { get; set; }

    /// <summary>
    /// Gets or sets the image URL.
    /// </summary>
    [Url]
    [MaxLength(500)]
    public string ImageUrl { get; set; }

    /// <summary>
    /// Gets or sets the car price.
    /// </summary>
    public required Price Price { get; set; }

    /// <summary>
    /// Gets or sets the license plate.
    /// </summary>
    public required LicensePlate LicensePlate { get; set; }

    /// <summary>
    /// Gets or sets the original reservation ID.
    /// </summary>
    public int OriginalReservationId { get; set; }

    /// <summary>
    /// Initializes a new instance of the Car class.
    /// </summary>
    public Car()
    {
        Title = string.Empty;
        Owner = string.Empty;
        OwnerEmail = string.Empty;
        Model = string.Empty;
        Description = string.Empty;
        ImageUrl = string.Empty;
        Brand = new Brand();
    }

    /// <summary>
    /// Initializes a new instance of the Car class with the specified creation command.
    /// </summary>
    /// <param name="command">The command containing the car's initial data.</param>
    [SetsRequiredMembers]
    public Car(CreateCarCommand command)
    {
        Title = command.Title;
        Owner = command.Owner;
        OwnerEmail = command.OwnerEmail;
        Year = new Year(command.Year);
        BrandId = command.BrandId;
        Model = command.Model;
        Description = command.Description ?? string.Empty;
        PdfCertification = new PdfCertification(command.PdfCertification);
        ImageUrl = command.ImageUrl ?? string.Empty;
        Price = new Price(command.Price);
        LicensePlate = new LicensePlate(command.LicensePlate);
        OriginalReservationId = command.OriginalReservationId;
        Brand = new Brand();
    }
}