public class CarResource
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Owner { get; set; }
    public required string OwnerEmail { get; set; }
    public int Year { get; set; }
    public int BrandId { get; set; }
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public required string LicensePlate { get; set; }
    public int OriginalReservationId { get; set; }
    public bool HasPdfCertification { get; set; }
}