using System;

namespace CertiWeb.API.Certifications.Domain.Model.ValueObjects;

/// <summary>
/// Represents a PDF certification as a value object stored as Base64.
/// </summary>
public record PdfCertification
{
    public string Base64Data { get; }

    public PdfCertification(string base64Data)
    {
        if (string.IsNullOrWhiteSpace(base64Data))
        {
            Base64Data = string.Empty;
            return;
        }
        
        string cleanedData = base64Data;
        if (base64Data.StartsWith("data:application/pdf;base64,"))
        {
            cleanedData = base64Data.Substring("data:application/pdf;base64,".Length);
            Console.WriteLine($"Removed data URL prefix. Original length: {base64Data.Length}, Cleaned length: {cleanedData.Length}");
        }
        
        if (cleanedData.Length < 10)
            throw new ArgumentException("PDF certification data is too short (minimum 10 characters)", nameof(base64Data));
        
        Base64Data = cleanedData;
    }

    /// <summary>
    /// Validates if the data is valid Base64 format (optional check).
    /// </summary>
    public bool IsValidBase64()
    {
        if (string.IsNullOrEmpty(Base64Data)) return true;
        
        try
        {
            Convert.FromBase64String(Base64Data);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static implicit operator string(PdfCertification certification) => certification.Base64Data;
    public static implicit operator PdfCertification(string value) => new(value);
}