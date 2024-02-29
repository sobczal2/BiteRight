// # ==============================================================================
// # Solution: BiteRight
// # File: UpdateProfileUsersTest.cs
// # Author: Łukasz Sobczak
// # Created: 13-02-2024
// # ==============================================================================

#region

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BiteRight.Application.Commands.Users.Onboard;
using BiteRight.Application.Commands.Users.UpdateProfile;
using BiteRight.Infrastructure.Configuration.Currencies;
using BiteRight.Web.Tests.Integration.TestHelpers;
using FluentAssertions;
using Xunit;

#endregion

namespace BiteRight.Web.Tests.Integration.Controllers.Users;

[Collection("DatabaseCollection")]
public class UpdateProfileUsersTest : IAsyncDisposable
{
    private readonly BiteRightBackendFactory _biteRightBackendFactory;
    private readonly HttpClient _client;

    public UpdateProfileUsersTest(
        BiteRightBackendFactory biteRightBackendFactory
    )
    {
        _biteRightBackendFactory = biteRightBackendFactory;
        _client = biteRightBackendFactory.CreateClient();
    }

    public async ValueTask DisposeAsync()
    {
        if (_client is IAsyncDisposable clientAsyncDisposable)
            await clientAsyncDisposable.DisposeAsync();
        else
            _client.Dispose();

        GC.SuppressFinalize(this);
    }

    private static string GetUrl()
    {
        return "api/Users/profile";
    }

    [Fact]
    public async Task Onboard_ReturnsOK_WhenRequestIsValid()
    {
        // Arrange
        var onboardRequest = new OnboardRequest(
            TestUsers.EmailVerifiedUser.Username,
            CurrencyConfiguration.USD.Id,
            TimeZoneInfo.Utc.Id
        );
        var onboardHttpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "api/Users/onboard")
            .AuthorizeAsEmailVerifiedUser();

        onboardHttpRequestMessage.Content = JsonContent.Create(onboardRequest);

        var onboardHttpResponse =
            await _client.SendAsync(onboardHttpRequestMessage);

        onboardHttpResponse.EnsureSuccessStatusCode();

        var updateProfileRequest = new UpdateProfileRequest(
            CurrencyConfiguration.USD.Id,
            TimeZoneInfo.FindSystemTimeZoneById("Europe/Warsaw").Id
        );

        var updateProfileHttpRequestMessage = new HttpRequestMessage(HttpMethod.Put, GetUrl())
            .AuthorizeAsEmailVerifiedUser();

        updateProfileHttpRequestMessage.Content = JsonContent.Create(updateProfileRequest);

        // Act
        var updateProfileHttpResponse =
            await _client.SendAsync(updateProfileHttpRequestMessage);

        // Assert
        updateProfileHttpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}