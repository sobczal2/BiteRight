namespace BiteRight.Options;

public class CacheOptions
{
    public const string SectionName = "Cache";
    
    public TimeSpan LanguageCacheDuration { get; set; }
}