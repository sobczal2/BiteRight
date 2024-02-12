// # ==============================================================================
// # Solution: BiteRight
// # File: UiCultureProvider.cs
// # Author: Łukasz Sobczak
// # Created: 12-01-2024
// # ==============================================================================

#region

using System.Globalization;
using BiteRight.Domain.Abstracts.Common;

#endregion

namespace BiteRight.Web.Providers;

public class UiCultureProvider : ICultureProvider
{
    public CultureInfo RequireCurrent()
    {
        return CultureInfo.CurrentUICulture;
    }
}