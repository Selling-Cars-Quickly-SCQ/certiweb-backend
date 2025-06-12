using System.Text.RegularExpressions;

namespace CertiWeb.API.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;

/// <summary>
///     Extension methods for <see cref="string" />.
/// </summary>
public static partial class StringExtensions
{
    /// <summary>
    ///     Converts the text to kebab case.
    /// </summary>
    /// <param name="text">string to convert</param>
    /// <returns>
    ///     The kebab case string.
    /// </returns>
    public static string ToKebabCase(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;
        return KebabCaseRegex().Replace(text, "-$1")
            .Trim()
            .ToLower();
    }

    [GeneratedRegex("(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])", RegexOptions.Compiled)]
    private static partial Regex KebabCaseRegex();
}