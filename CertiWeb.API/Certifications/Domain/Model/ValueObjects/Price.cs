namespace CertiWeb.API.Certifications.Domain.Model.ValueObjects;

/// <summary>
/// Represents a price as a value object.
/// </summary>
public record Price
{
    public decimal Value { get; }
    public string Currency { get; }

    // Remove the default parameter
    public Price(decimal value, string currency)
    {
        if (value < 0)
            throw new ArgumentException("Price cannot be negative", nameof(value));
        
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency cannot be empty", nameof(currency));
        
        Value = value;
        Currency = currency;
    }
    
    public Price(decimal value) : this(value, "SOL")
    {
    }

    public static implicit operator decimal(Price price) => price.Value;
    public static implicit operator Price(decimal value) => new(value, "SOL");
}