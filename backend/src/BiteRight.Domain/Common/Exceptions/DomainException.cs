using System;

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