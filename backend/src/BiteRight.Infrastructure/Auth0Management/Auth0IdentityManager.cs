using System.Text.Json;
using Auth0.ManagementApi;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Users;
using BiteRight.Options;
using Microsoft.Extensions.Options;

namespace BiteRight.Infrastructure.Auth0Management;

public class Auth0IdentityManager : IIdentityManager
{
    private readonly Auth0Options _auth0Options;
    private readonly IDateTimeProvider _dateTimeProvider;
    private IManagementApiClient? _managementApiClient;
    private DateTime? _managementApiTokenExpiresAt;

    public Auth0IdentityManager(
        IOptions<Auth0Options> auth0Options,
        IDateTimeProvider dateTimeProvider
    )
    {
        _auth0Options = auth0Options.Value;
        _dateTimeProvider = dateTimeProvider;
    }

    private async Task EnsureManagementApiToken(
        CancellationToken cancellationToken = default
    )
    {
        if (_managementApiTokenExpiresAt is null ||
            _managementApiTokenExpiresAt < DateTime.UtcNow - TimeSpan.FromSeconds(30))
        {
            using var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://{_auth0Options.Domain}/oauth/token");
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = _auth0Options.ManagementApiClientId,
                ["client_secret"] = _auth0Options.ManagementApiClientSecret,
                ["audience"] = _auth0Options.GetManagementApiAudience()
            });
            var response = await client.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();

            var getTokenResponse = JsonSerializer.Deserialize<Auth0GetTokenResponse>(
                await response.Content.ReadAsStringAsync(cancellationToken)
            );

            if (getTokenResponse is null)
            {
                throw new InvalidOperationException("Failed to deserialize token response.");
            }

            _managementApiClient = new ManagementApiClient(
                getTokenResponse.AccessToken,
                new Uri(_auth0Options.GetManagementApiAudience())
            );

            _managementApiTokenExpiresAt = _dateTimeProvider.UtcNow.AddSeconds(getTokenResponse.ExpiresIn);
        }
    }

    public async Task<(Email email, bool isVerified)> GetEmail(
        IdentityId identityId,
        CancellationToken cancellationToken = default
    )
    {
        await EnsureManagementApiToken(cancellationToken);

        var user = await _managementApiClient!.Users.GetAsync(identityId.Value, cancellationToken: cancellationToken);

        if (user is null)
        {
            throw new InvalidOperationException("User not found.");
        }

        return (Email.Create(user.Email), user.EmailVerified ?? false);
    }
}