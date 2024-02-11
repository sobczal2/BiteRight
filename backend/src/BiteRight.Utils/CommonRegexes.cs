using System.Text.RegularExpressions;

namespace BiteRight.Utils;

public static class CommonRegexes
{
    public static Regex LowercaseLetters = new(@"^\p{Ll}*$", RegexOptions.Compiled);
    public static Regex UppercaseLetters = new(@"^\p{Lu}*$", RegexOptions.Compiled);
    public static Regex Letters = new(@"^\p{L}*$", RegexOptions.Compiled);
    public static Regex Numbers = new(@"^\p{Nd}*$", RegexOptions.Compiled);
    public static Regex Alphanumeric = new(@"^[\p{L}\p{Nd}]*$", RegexOptions.Compiled);
    public static Regex AlphanumericWithSpaces = new(@"^[\p{L}\p{Nd}\s]*$", RegexOptions.Compiled);
    public static Regex LettersWithSpaces = new(@"^[\p{L}\s]*$", RegexOptions.Compiled);

    public static Regex AlphanumericWithSpacesAndSpecialCharacters =
        new(@"^[\p{L}\p{Nd}\s.,!?""'-()]*$", RegexOptions.Compiled);

    public static Regex AlphanumericWithHuphensAndUnderscores = new(@"^[\p{L}\p{Nd}-_]*$", RegexOptions.Compiled);
}