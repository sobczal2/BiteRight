// # ==============================================================================
// # Solution: BiteRight
// # File: Auth0Options.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

namespace BiteRight.Options;

public class Auth0Options
{
    public const string SectionName = "Auth0";
    public string Domain { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public string ManagementApiClientId { get; set; } = default!;
    public string ManagementApiClientSecret { get; set; } = default!;
    public string MobileClientId { get; set; } = default!;

    public string GetManagementApiAudience()
    {
        return $"https://{Domain}/api/v2/";
    }
}