// # ==============================================================================
// # Solution: BiteRight
// # File: Auth0GetTokenResponse.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Text.Json.Serialization;

#endregion

namespace BiteRight.Infrastructure.Auth0Management;

public class Auth0GetTokenResponse
{
    [JsonPropertyName("access_token")] public string AccessToken { get; set; } = default!;

    [JsonPropertyName("scope")] public string Scope { get; set; } = default!;

    [JsonPropertyName("expires_in")] public int ExpiresIn { get; set; }

    [JsonPropertyName("token_type")] public string TokenType { get; set; } = default!;
}