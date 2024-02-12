// # ==============================================================================
// # Solution: BiteRight
// # File: CacheOptions.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;

#endregion

namespace BiteRight.Options;

public class CacheOptions
{
    public const string SectionName = "Cache";

    public TimeSpan LanguageCacheDuration { get; set; }
    public TimeSpan CategoryCacheDuration { get; set; }
    public TimeSpan CurrencyCacheDuration { get; set; }
    public TimeSpan CountryCacheDuration { get; set; }
    public TimeSpan UserCacheDuration { get; set; }
}