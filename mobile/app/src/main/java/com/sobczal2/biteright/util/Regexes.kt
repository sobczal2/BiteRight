package com.sobczal2.biteright.util

object CommonRegexes {
    val lowercaseLetters = Regex("""^\p{Ll}*$""")
    val uppercaseLetters = Regex("""^\p{Lu}*$""")
    val letters = Regex("""^\p{L}*$""")
    val numbers = Regex("""^\p{Nd}*$""")
    val alphanumeric = Regex("""^[\p{L}\p{Nd}]*$""")
    val alphanumericWithSpaces = Regex("""^[\p{L}\p{Nd}\s]*$""")
    val lettersWithSpaces = Regex("""^[\p{L}\s]*$""")

    val alphanumericWithSpacesAndSpecialCharacters =
        Regex("""^[\p{L}\p{Nd}\s.,!?""'-()]*$""")

    val alphanumericWithHyphensAndUnderscores = Regex("""^[\p{L}\p{Nd}-_]*$""")
}
