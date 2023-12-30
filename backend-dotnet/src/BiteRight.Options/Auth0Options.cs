namespace BiteRight.Options;

public class Auth0Options
{
    public const string SectionName = "Auth0";
    public string Domain { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public string ManagementApiClientId { get; set; } = default!;
    public string ManagementApiClientSecret { get; set; } = default!;
    public string ManagementApiDomain { get; set; } = default!;
    
    public string GetManagementApiAudience()
    {
        return $"https://{ManagementApiDomain}/api/v2/";
    }
}