// # ==============================================================================
// # Solution: BiteRight
// # File: MeUsersTests.cs
// # Author: Łukasz Sobczak
// # Created: 13-02-2024
// # ==============================================================================

#region

using System;
using System.Net.Http;
using System.Threading.Tasks;
using BiteRight.Web.Tests.Integration.TestHelpers;
using Xunit;

#endregion

namespace BiteRight.Web.Tests.Integration.Controllers.Users;

[Collection("DatabaseCollection")]
public class MeUsersTests : IAsyncDisposable
{
    private readonly BiteRightBackendFactory _biteRightBackendFactory;
    private readonly HttpClient _client;

    public MeUsersTests(
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
        return "api/Users/me";
    }

    [Fact]
    public async Task Me_ReturnsOK_WhenRequestIsValid()
    {
        // Arrange
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, GetUrl())
            .AuthorizeAsOnboardedUser();

        // Act
        var httpResponse =
            await _client.SendAsync(httpRequestMessage);

        // Assert
        httpResponse.EnsureSuccessStatusCode();
    }
}