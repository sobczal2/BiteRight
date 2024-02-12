// # ==============================================================================
// # Solution: BiteRight
// # File: ICultureProvider.cs
// # Author: Łukasz Sobczak
// # Created: 12-01-2024
// # ==============================================================================

#region

using System.Globalization;

#endregion

namespace BiteRight.Domain.Abstracts.Common;

public interface ICultureProvider
{
    CultureInfo RequireCurrent();
}