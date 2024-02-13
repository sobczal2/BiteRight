// # ==============================================================================
// # Solution: BiteRight
// # File: CodeInvalidLengthException.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Domain.Common.Exceptions;

#endregion

namespace BiteRight.Domain.Languages.Exceptions;

public class CodeInvalidLengthException : BusinessRuleDomainException
{
    public CodeInvalidLengthException(
        int expectedLength
    )
    {
        ExpectedLength = expectedLength;
    }

    public int ExpectedLength { get; }
}