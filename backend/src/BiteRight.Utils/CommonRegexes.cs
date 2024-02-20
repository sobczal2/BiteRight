// # ==============================================================================
// # Solution: BiteRight
// # File: CommonRegexes.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Text.RegularExpressions;

#endregion

namespace BiteRight.Utils;

public static class CommonRegexes
{
    public static readonly Regex LowercaseLetters = new(@"^\p{Ll}*$", RegexOptions.Compiled);
    public static readonly Regex UppercaseLetters = new(@"^\p{Lu}*$", RegexOptions.Compiled);
    public static readonly Regex Letters = new(@"^\p{L}*$", RegexOptions.Compiled);
    public static readonly Regex Numbers = new(@"^\p{Nd}*$", RegexOptions.Compiled);
    public static readonly Regex Alphanumeric = new(@"^[\p{L}\p{Nd}]*$", RegexOptions.Compiled);
    public static readonly Regex AlphanumericWithSpaces = new(@"^[\p{L}\p{Nd}\s]*$", RegexOptions.Compiled);
    public static readonly Regex LettersWithSpaces = new(@"^[\p{L}\s]*$", RegexOptions.Compiled);

    public static readonly Regex AlphanumericWithSpacesAndSpecialCharacters =
        new(@"^[\p{L}\p{Nd}\s.,!?""'-()]*$", RegexOptions.Compiled);

    public static readonly Regex AlphanumericWithHuphensAndUnderscores = new(@"^[\p{L}\p{Nd}-_]*$", RegexOptions.Compiled);
}