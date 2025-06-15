namespace CertiWeb.API.Certifications.Domain.Model.ValueObjects;

/// <summary>
/// Represents a vehicle license plate as a value object.
/// </summary>
public record LicensePlate
{
    public string Value { get; }

    public LicensePlate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("License plate cannot be empty", nameof(value));
        
        if (value.Length < 6 || value.Length > 10)
            throw new ArgumentException("License plate must be between 6 and 10 characters", nameof(value));
        
        Value = value.ToUpperInvariant();
    }

    public static implicit operator string(LicensePlate licensePlate) => licensePlate.Value;
    public static implicit operator LicensePlate(string value) => new(value);
}