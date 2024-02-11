using System.Text.RegularExpressions;

namespace BiteRight.Utils;

public static class CommonRegexes
{
    public static Regex LowercaseLetters = new Regex(@"^\p{Ll}*$", RegexOptions.Compiled);
    public static Regex UppercaseLetters = new Regex(@"^\p{Lu}*$", RegexOptions.Compiled);
    public static Regex Letters = new Regex(@"^\p{L}*$", RegexOptions.Compiled);
    public static Regex Numbers = new Regex(@"^\p{Nd}*$", RegexOptions.Compiled);
    public static Regex Alphanumeric = new Regex(@"^[\p{L}\p{Nd}]*$", RegexOptions.Compiled);
    public static Regex AlphanumericWithSpaces = new Regex(@"^[\p{L}\p{Nd}\s]*$", RegexOptions.Compiled);
    public static Regex LettersWithSpaces = new Regex(@"^[\p{L}\s]*$", RegexOptions.Compiled);
    public static Regex AlphanumericWithSpacesAndSpecialCharacters = new Regex(@"^[\p{L}\p{Nd}\s.,!?""'-()]*$", RegexOptions.Compiled);
    public static Regex AlphanumericWithHuphensAndUnderscores = new Regex(@"^[\p{L}\p{Nd}-_]*$", RegexOptions.Compiled);
}
