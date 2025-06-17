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
    [MaxLength(100)]
    public string OwnerEmail { get; set; }

    /// <summary>
    /// Gets or sets the car year.
    /// </summary>
    public Year Year { get; set; }

    /// <summary>
    /// Gets or sets the brand ID.
    /// </summary>
    public int BrandId { get; set; }

    /// <summary>
    /// Gets or sets the brand.
    /// </summary>
    [ForeignKey("BrandId")]
    public Brand? Brand { get; set; } = null!;

    /// <summary>
    /// Gets or sets the car model.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Model { get; set; }

    /// <summary>
    /// Gets or sets the car description.
    /// </summary>
    [MaxLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the PDF certification.
    /// </summary>
    public PdfCertification PdfCertification { get; set; }

    /// <summary>
    /// Gets or sets the car image URL.
    /// </summary>
    [MaxLength(500)]
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Gets or sets the car price.
    /// </summary>
    public Price Price { get; set; }

    /// <summary>
    /// Gets or sets the license plate.
    /// </summary>
    public LicensePlate LicensePlate { get; set; }

    /// <summary>
    /// Gets or sets the original reservation ID.
    /// </summary>
    public int OriginalReservationId { get; set; }

    /// <summary>
    /// Protected constructor for EF Core.
    /// </summary>
    protected Car()
    {
        Title = string.Empty;
        Owner = string.Empty;
        OwnerEmail = string.Empty;
        Model = string.Empty;
        Year = null!;
        PdfCertification = null!;
        Price = null!;
        LicensePlate = null!;
    }

    /// <summary>
    /// Initializes a new instance of the Car class.
    /// </summary>
    /// <param name="command">The command containing the car data.</param>
    public Car(CreateCarCommand command)
    {
        Title = command.Title;
        Owner = command.Owner;
        OwnerEmail = command.OwnerEmail;
        Year = new Year(command.Year);
        BrandId = command.BrandId;
        Model = command.Model;
        Description = command.Description;
        PdfCertification = new PdfCertification(command.PdfCertification);
        ImageUrl = command.ImageUrl;
        Price = new Price(command.Price);
        LicensePlate = new LicensePlate(command.LicensePlate);
        OriginalReservationId = command.OriginalReservationId;
    }
}