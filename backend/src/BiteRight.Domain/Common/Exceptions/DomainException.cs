// # ==============================================================================
// # Solution: BiteRight
// # File: DomainException.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;

#endregion

namespace BiteRight.Domain.Common.Exceptions;

public class DomainException : Exception
{
    public DomainException(
        string message
    )
        : base(message)
    {
    }
}