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
            throw new ArgumentException("PDF certification data cannot be empty", nameof(base64Data));
        
        if (base64Data.Length < 10)
            throw new ArgumentException("PDF certification data is too short", nameof(base64Data));
        
        Base64Data = base64Data;
    }

    /// <summary>
    /// Validates if the data is valid Base64 format (optional check).
    /// </summary>
    public bool IsValidBase64()
    {
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

    public static implicit operator string(PdfCertification pdf) => pdf.Base64Data;
    public static implicit operator PdfCertification(string base64Data) => new(base64Data);
}