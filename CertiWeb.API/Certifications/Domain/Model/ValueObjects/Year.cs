namespace CertiWeb.API.Certifications.Domain.Model.ValueObjects;

/// <summary>
/// Represents a vehicle year as a value object.
/// </summary>
public record Year
{
    public int Value { get; }

    public Year(int value)
    {
        var currentYear = DateTime.Now.Year;
        if (value < 1900 || value > currentYear + 1)
            throw new ArgumentException($"Year must be between 1900 and {currentYear + 1}", nameof(value));
        
        Value = value;
    }

    public static implicit operator int(Year year) => year.Value;
    public static implicit operator Year(int value) => new(value);
}