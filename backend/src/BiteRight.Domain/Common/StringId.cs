// # ==============================================================================
// # Solution: BiteRight
// # File: StringId.cs
// # Author: Łukasz Sobczak
// # Created: 12-01-2024
// # ==============================================================================

namespace BiteRight.Domain.Common;

public class StringId : Id<string>
{
    public StringId(
        string value
    )
        : base(value)
    {
    }
}